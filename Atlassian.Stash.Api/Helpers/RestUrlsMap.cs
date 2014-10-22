using Atlassian.Stash.Api.Entities;
using System;
using System.Linq;

namespace Atlassian.Stash.Api.Helpers
{
    internal static class RestUrlsMap
    {
        // todo: add unit tests
        public static string GetSingleTUrl<T>(params string[] inputs)
        {
            Type T_Type = typeof(T);

            if (T_Type == typeof(Project))
                return GetProjectUrl(inputs);
            else if (T_Type == typeof(Repository))
                return GetRepositoryUrl(inputs);

            throw new ArgumentException(String.Format("Unable to find method to handle the requested type: '{0}'", T_Type.ToString()));
        }

        public static string GetManyTUrl<T>(RequestOptions requestOptions, params string[] inputs)
        {
            Type T_Type = typeof(T);

            if (T_Type == typeof(Project))
                return GetProjectsUrl(requestOptions);
            else if (T_Type == typeof(Repository))
                return GetRepositoriesUrl(requestOptions, inputs);
            else if (T_Type.Equals(typeof(Tag)))
                return GetTagsUrl(requestOptions, inputs);

            throw new ArgumentException(String.Format("Unable to find method to handle the requested type: '{0}'", T_Type.ToString()));
        }

        private static string GetProjectsUrl(RequestOptions requestOptions)
        {
            return FormatRestApiUrl("/rest/api/1.0/projects", requestOptions);
        }

        private static string GetProjectUrl(params string[] inputs)
        {
            StringParamsValidator(1, inputs);

            return String.Format("/rest/api/1.0/projects/{0}", inputs);
        }

        private static string GetRepositoriesUrl(RequestOptions requestOptions, params string[] inputs)
        {
            StringParamsValidator(1, inputs);

            return FormatRestApiUrl("/rest/api/1.0/projects/{0}/repos", requestOptions, inputs);
        }

        private static string GetRepositoryUrl(params string[] inputs)
        {
            StringParamsValidator(2, inputs);

            return String.Format("/rest/api/1.0/projects/{0}/repos/{1}", inputs);
        }

        private static string GetTagsUrl(RequestOptions requestOptions, params string[] inputs)
        {
            StringParamsValidator(2, inputs);

            return FormatRestApiUrl("/rest/api/1.0/projects/{0}/repos/{1}/tags", requestOptions, inputs);
        }

        private static void StringParamsValidator(int validParamCount, params string[] inputs)
        {
            if (inputs.Length != validParamCount || inputs.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentException(string.Format("Wrong number of parameters passed, expecting exactly '{0}' parameters", validParamCount));
        }

        // todo: refactor, since I don't like this logic. At least use string builder
        // todo: add unit tests
        private static string FormatRestApiUrl(string restUrl, RequestOptions requestOptions, params string[] inputs)
        {
            string resultingUrl = String.Format(restUrl, inputs);

            if (requestOptions != null)
            {
                string partialUrl = "";
                if (requestOptions.Limit != null && requestOptions.Limit.HasValue && requestOptions.Limit.Value > 0)
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) ? "?" : "&";
                    partialUrl += string.Format("limit={0}", requestOptions.Limit.Value);
                }

                if (requestOptions.Start != null && requestOptions.Start.HasValue && requestOptions.Start.Value >= 0)
                {
                    partialUrl += string.IsNullOrWhiteSpace(partialUrl) ? "?" : "&";
                    partialUrl += string.Format("start={0}", requestOptions.Start.Value);
                }

                resultingUrl += partialUrl;
            }

            return resultingUrl;
        }
    }
}
