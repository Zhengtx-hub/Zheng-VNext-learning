using Microsoft.Extensions.Options;
using Nest;
using SearchService.Domain;

namespace SearchService.Infrastructure
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IElasticClient elasticClient;
        private readonly string episodeIndex;
        public SearchRepository(IElasticClient elasticClient,IOptions<ElasticSearchOptions> options)
        {
            this.elasticClient = elasticClient;
            this.episodeIndex = options.Value.EpisodeIndex;
 
        }

        public Task DeleteAsync(Guid episodeId)
        {
            elasticClient.DeleteByQuery<Episode>(q => q
               .Index(episodeIndex)
               .Query(rq => rq.Term(f => f.Id, "elasticsearch.pm")));
            //因为有可能文档不存在，所以不检查结果
            //如果Episode被删除，则把对应的数据也从Elastic Search中删除
            return elasticClient.DeleteAsync(new DeleteRequest(episodeIndex, episodeId));
        }

        public async Task<SearchEpisodesResponse> SearchEpisodes(string Keyword, int PageIndex, int PageSize)
        {
            int from = PageSize * (PageIndex - 1);
            string kw = Keyword;
            Func<QueryContainerDescriptor<Episode>, QueryContainer> query = (q) =>
                          q.Match(mq => mq.Field(f => f.CnName).Query(kw))
                          || q.Match(mq => mq.Field(f => f.EngName).Query(kw))
                          || q.Match(mq => mq.Field(f => f.PlainSubtitle).Query(kw));
            Func<HighlightDescriptor<Episode>, IHighlight> highlightSelector = h => h
                .Fields(fs => fs.Field(f => f.PlainSubtitle));
            var result = await this.elasticClient.SearchAsync<Episode>(s => s.Index(episodeIndex).From(from)
                .Size(PageSize).Query(query).Highlight(highlightSelector));
            if (result == null || !result.IsValid)
            {
                throw new ApplicationException("ElasticSearch 查询失败", result?.OriginalException);
            }
            List<Episode> episodes = new List<Episode>();
            foreach (var hit in result.Hits)
            {
                string highlightedSubtitle;
                //如果没有预览内容，则显示前50个字
                if (hit.Highlight.ContainsKey("plainSubtitle"))
                {
                    highlightedSubtitle = string.Join("\r\n", hit.Highlight["plainSubtitle"]);
                }
                else
                {
                    highlightedSubtitle = hit.Source.PlainSubtitle.Cut(50);
                }
                var episode = hit.Source with { PlainSubtitle = highlightedSubtitle };
                episodes.Add(episode);
            }
            return new SearchEpisodesResponse(episodes, result.Total);
        }

        public async Task UpsertAsync(Episode episode)
        {
            var response = await elasticClient.IndexAsync(episode, idx => idx.Index(episodeIndex).Id(episode.Id));//Upsert:Update or Insert
            if (!response.IsValid)
            {
                throw new ApplicationException(response.DebugInformation);
            }
        }
    }
}
