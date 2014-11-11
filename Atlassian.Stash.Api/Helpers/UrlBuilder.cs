using System;
using System.Linq;
using System.Web;

namespace Atlassian.Stash.Api.Helpers
{
    internal class UrlBuilder
    {
        // todo: refactor, since I don't like this logic. At least use string builder
        // todo: UrlEncode when necessary
        public static string FormatRestApiUrl(string restUrl, RequestOptions requestOptions = null, params string[] inputs)
        {
            StringParamsValidator(inputs.Length, inputs);

            string resultingUrl = String.Format(restUrl, UrlEscapeParams(inputs));

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

        private static void StringParamsValidator(int validParamCount, params string[] inputs)
        {
            if (inputs.Length != validParamCount || inputs.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentException(string.Format("Wrong number of parameters passed, expecting exactly '{0}' parameters", validParamCount));
        }

        private static string[] UrlEscapeParams(string[] inputs)
        {
            string[] result = new string[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
                result[i] = HttpUtility.UrlEncode(inputs[i]);


            return result;
        }
    }
}
