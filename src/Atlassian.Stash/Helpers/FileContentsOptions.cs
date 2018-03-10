namespace Atlassian.Stash.Helpers
{
    /// <summary>
    /// Extends <see cref="RequestOptions"/> to include options for file content requests.
    /// </summary>
    public class FileContentsOptions : RequestOptions
    {
        /// <summary>
        /// Gets or sets the Blame Option. True indicates blame information should be included with the response.
        /// </summary>
        public bool? Blame { get; set; }

        /// <summary>
        /// Gets or sets the Content Option. True indicates file content should be included with the response.
        /// </summary>
        public bool? Content { get; set; }
    }
}