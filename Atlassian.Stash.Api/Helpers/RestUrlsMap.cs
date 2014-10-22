using Atlassian.Stash.Api.Entities;
using System;
using System.Linq;

namespace Atlassian.Stash.Api.Helpers
{
    public static class RestUrlsMap
    {
        public static string GetProjectsUrl()
        {
            return "/rest/api/1.0/projects";
        }

        public static string GetProjectUrl(params string[] inputs)
        {
            StringParamsValidator(1, inputs);

            return String.Format("/rest/api/1.0/projects/{0}", inputs);
        }

        public static string GetRepositoriesUrl(params string[] inputs)
        {
            StringParamsValidator(1, inputs);

            return String.Format("/rest/api/1.0/projects/{0}/repos", inputs);
        }

        public static string GetRepositoryUrl(params string[] inputs)
        {
            StringParamsValidator(2, inputs);

            return String.Format("/rest/api/1.0/projects/{0}/repos/{1}", inputs);
        }

        public static string GetTagsUrl(params string[] inputs)
        {
            StringParamsValidator(2, inputs);

            return String.Format("/rest/api/1.0/projects/{0}/repos/{1}/tags", inputs);
        }

        private static void StringParamsValidator(int validParamCount, params string[] inputs)
        {
            if (inputs.Length != validParamCount || inputs.Any(x => string.IsNullOrWhiteSpace(x)))
                throw new ArgumentException(string.Format("Wrong number of parameters passed, expecting exactly '{0}' parameters", validParamCount));
        }

        // todo: add unit tests
        public static string GetSingleTUrl<T>(params string[] inputs)
        {
            Type T_Type = typeof(T);

            if (T_Type == typeof(Project))
            {
                return GetProjectUrl(inputs);
            }

            else if (T_Type == typeof(Repository))
            {
                return GetRepositoryUrl(inputs);
            }

            throw new ArgumentException(String.Format("Unable to find method to handle the requested type: '{0}'", T_Type.ToString()));
        }

        public static string GetManyTUrl<T>(params string[] inputs)
        {
            Type T_Type = typeof(T);

            if (T_Type == typeof(Project))
            {
                return GetProjectsUrl();
            }
            else if (T_Type == typeof(Repository))
            {
                return GetRepositoriesUrl(inputs);
            }
            else if (T_Type.Equals(typeof(Tag)))
            {
                return GetTagsUrl(inputs);
            }

            throw new ArgumentException(String.Format("Unable to find method to handle the requested type: '{0}'", T_Type.ToString()));
        }
    }
}
