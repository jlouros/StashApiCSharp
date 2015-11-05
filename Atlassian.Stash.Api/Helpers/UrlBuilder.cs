﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Atlassian.Stash.Api.Helpers
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
					    sb.Append(queryParam.Key + "=" + HttpUtility.UrlEncode(queryParam.Value));
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
