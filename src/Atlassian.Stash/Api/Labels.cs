using System.Threading.Tasks;
using Atlassian.Stash.Entities;
using Atlassian.Stash.Helpers;
using Atlassian.Stash.Workers;

namespace Atlassian.Stash.Api
{
    public class Labels
    {
        private const string MANY_LABELS = "rest/api/1.0/labels";
        private const string ONE_LABEL = "rest/api/1.0/labels/{0}";
        private const string LABELED_REPOSITORY = "rest/api/1.0/labels/{0}/labeled";

        private HttpCommunicationWorker _httpWorker;

        internal Labels(HttpCommunicationWorker httpWorker)
        {
            _httpWorker = httpWorker;
        }

        public async Task<ResponseWrapper<Label>> Get(RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_LABELS, requestOptions);

            ResponseWrapper<Label> response = await _httpWorker.GetAsync<ResponseWrapper<Label>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Label>> Get(string prefix, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(MANY_LABELS + "?prefix={0}", requestOptions, prefix);

            ResponseWrapper<Label> response = await _httpWorker.GetAsync<ResponseWrapper<Label>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<Label>> GetByLabelName(string labelName, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(ONE_LABEL, requestOptions, labelName);

            ResponseWrapper<Label> response = await _httpWorker.GetAsync<ResponseWrapper<Label>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<LabelRepository>> GetRepositories(string labelName, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(LABELED_REPOSITORY, requestOptions, labelName);

            ResponseWrapper<LabelRepository> response = await _httpWorker.GetAsync<ResponseWrapper<LabelRepository>>(requestUrl).ConfigureAwait(false);

            return response;
        }

        public async Task<ResponseWrapper<LabelRepository>> GetRepositories(string labelName, string type, RequestOptions requestOptions = null)
        {
            string requestUrl = UrlBuilder.FormatRestApiUrl(LABELED_REPOSITORY + "?type={1}", requestOptions, labelName, type);

            ResponseWrapper<LabelRepository> response = await _httpWorker.GetAsync<ResponseWrapper<LabelRepository>>(requestUrl).ConfigureAwait(false);

            return response;
        }
    }
}