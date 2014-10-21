using Atlassian.Stash.Api.Entities;
using System;

namespace Atlassian.Stash.Api.Helpers
{
    public static class RestUrlsMap
    {
        public static string GetProjectsUrl()
        {
            return "/rest/api/1.0/projects";
        }

        public static string GetProjectsUrl(params object[] inputs)
        {
            return String.Format("/rest/api/1.0/projects", inputs);
        }

        public static string GetRepositoriesUrl(string projectKey)
        {
            return String.Format("/rest/api/1.0/projects/{0}/repos", projectKey);
        }

        public static string GetRepositoriesUrl(params object[] inputs)
        {
            return String.Format("/rest/api/1.0/projects/{0}/repos", inputs);
        }

        public static string GetTagsUrl(string projectKey, string repositorySlug)
        {
            return String.Format("/rest/api/1.0/projects/{0}/repos/{1}/tags", projectKey, repositorySlug);
        }

        public static string GetTagsUrl(params object[] inputs)
        {
            return String.Format("/rest/api/1.0/projects/{0}/repos/{1}/tags", inputs);
        }

        public static string GetTUrl<T>(params string[] inputs)
        {
            Type T_Type = typeof(T);

            if (T_Type == typeof(Project))
            {
                return GetProjectsUrl(inputs);
            }
            else if (T_Type == typeof(Repository))
            {
                return GetRepositoriesUrl(inputs);
            }
            else if (T_Type.Equals(typeof(Tag)))
            {
                return GetTagsUrl(inputs);
            }

            throw new ArgumentException(String.Format("Unsupported type: '{0}'", T_Type.ToString()));
        }
    }
}
