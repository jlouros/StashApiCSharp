using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlassian.Stash.Helpers
{
    internal class UrlBuilder
    {

        internal class FluentUrl
        {
            private readonly string m_url;
            private readonly Dictionary<string, string> m_queryParams = new Dictionary<string, string>();

            public FluentUrl(string url)
            {
                m_url = url;
            }

            public FluentUrl WithOptions(RequestOptions options)
            {
                if (options == null)
                {
                    return this;
                }

                if (options.Limit.HasValue && options.Limit > 0)
                {
                    m_queryParams["limit"] = options.Limit.ToString();
                }

                if (options.Start.HasValue && options.Start >= 0)
                {
                    m_queryParams["start"] = options.Start.ToString();
                }

                if (!String.IsNullOrWhiteSpace(options.At))
                {
                    m_queryParams["at"] = options.At;
                }

                return this;

            }

            public static implicit operator String(FluentUrl url)
            {
                return url.ToString();
            }

            public FluentUrl WithQueryParam(string key, string value)
            {
                if (String.IsNullOrWhiteSpace(key) || String.IsNullOrWhiteSpace(value))
                {
                    return this;
                }

                m_queryParams[key] = value;

                return this;

            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(m_url);

                //Format query params
                if (m_queryParams.Any())
                {
                    sb.Append('?');
                    foreach (var queryParam in m_queryParams)
                    {
                        sb.Append(queryParam.Key + "=" + Uri.EscapeDataString(queryParam.Value));
                        sb.Append('&');
                    }
                    sb.Length -= 1;
                }

                return sb.ToString();

            }
        }

        public static FluentUrl ToRestApiUrl(string restUrl)
        {
            return new FluentUrl(restUrl);
        }

        // todo: refactor, since I don't like this logic. At least use string builder
        // todo: UrlEncode when necessary
        public static string FormatRestApiUrl(string restUrl, RequestOptions requestOptions = null, params string[] inputs)
        {
            return FormatRestApiUrl(restUrl, true, requestOptions, inputs);
        }

        public static string FormatRestApiUrl(string restUrl, bool escapeUrlData, RequestOptions requestOptions = null, params string[] inputs)
        {
            StringParamsValidator(inputs.Length, inputs);

            string resultingUrl = escapeUrlData ? String.Format(restUrl, UrlEscapeDataParams(inputs)) : String.Format(restUrl, UrlEscapeUriParams(inputs));

            if (requestOptions != null)
            {
                string partialUrl = "";
                bool urlHasQueryParams = restUrl.IndexOf('?') > -1;

                if (requestOptions.Limit != null && requestOptions.Limit.HasValue && requestOptions.Limit.Value > 0)
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("limit={0}", requestOptions.Limit.Value);
                }

                if (requestOptions.Start != null && requestOptions.Start.HasValue && requestOptions.Start.Value >= 0)
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("start={0}", requestOptions.Start.Value);
                }

                if (!String.IsNullOrWhiteSpace(requestOptions.At))
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("at={0}", requestOptions.At);
                }

                resultingUrl += partialUrl;
            }

            return resultingUrl;
        }

        public static string FormatRestApiUrlWithCommitOptions(string restUrl, RequestOptions requestOptions = null, RequestOptionsForCommits commitRequestOptions = null, params string[] inputs)
        {
            string resultingUrl = FormatRestApiUrl(restUrl, requestOptions, inputs);

            if (commitRequestOptions != null)
            {
                string partialUrl = "";
                bool urlHasQueryParams = restUrl.IndexOf('?') > -1;

                if (!string.IsNullOrWhiteSpace(commitRequestOptions.Path))
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("path={0}", commitRequestOptions.Path);
                }

                if (!string.IsNullOrWhiteSpace(commitRequestOptions.Since))
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("since={0}", commitRequestOptions.Since);
                }

                if (!string.IsNullOrWhiteSpace(commitRequestOptions.Until))
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += string.Format("until={0}", commitRequestOptions.Until);
                }

                if (commitRequestOptions.WithCounts)
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) && !urlHasQueryParams ? "?" : "&";
                    partialUrl += "withCounts=true";
                }

                resultingUrl += partialUrl;
            }

            return resultingUrl;
        }

        private static void StringParamsValidator(int validParamCount, params string[] inputs)
        {
            if (inputs.Length != validParamCount || inputs.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException(string.Format("Wrong number of parameters passed, expecting exactly '{0}' parameters", validParamCount));
            }
        }

        // todo: Review when it's appropriate to use Uri.EscapeDataString and Uri.EscapeUriString

        private static string[] UrlEscapeDataParams(string[] inputs)
        {
            string[] result = new string[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                result[i] = Uri.EscapeDataString(inputs[i]);
            }


            return result;
        }

        private static string[] UrlEscapeUriParams(string[] inputs)
        {
            string[] result = new string[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                result[i] = Uri.EscapeUriString(inputs[i]);
            }


            return result;
        }
    }
}
