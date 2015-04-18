using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Atlassian.Stash.Api.Helpers;

namespace Atlassian.Stash.Api.UnitTests
{
    [TestClass]
    public class UrlBuilderTests
    {
        [TestMethod]
        public void FormatRestApiUrl_Empty_Url()
        {
            string result = UrlBuilder.FormatRestApiUrl(string.Empty);

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FormatRestApiUrl_Null_Url()
        {
            UrlBuilder.FormatRestApiUrl(null);
        }

        [TestMethod]
        public void FormatRestApiUrl_Empty_Url_Empty_RequestOptions()
        {
            string result = UrlBuilder.FormatRestApiUrl(string.Empty, new RequestOptions());

            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatRestApiUrl_Empty_Url_Empty_RequestOptions_Empty_Param()
        {
            UrlBuilder.FormatRestApiUrl(string.Empty, new RequestOptions(), "");
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void FormatRestApiUrl_Validate_Single_Param_Count()
        {
            UrlBuilder.FormatRestApiUrl("{0}", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatRestApiUrl_Validate_Multiple_Param_Count()
        {
            UrlBuilder.FormatRestApiUrl("{1}", null, "", "2nd");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void FormatRestApiUrl_Empty_Url_Empty_RequestOptions_Null_Param()
        {
            UrlBuilder.FormatRestApiUrl(string.Empty, new RequestOptions(), null);
        }

        [TestMethod]
        public void FormatRestApiUrl_Simple_Url_Empty_RequestOptions()
        {
            string result = UrlBuilder.FormatRestApiUrl("none", new RequestOptions());

            Assert.AreEqual("none", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Simple_Url_WithLimit_RequestOptions()
        {
            string result = UrlBuilder.FormatRestApiUrl("none", new RequestOptions { Limit = 1 });

            Assert.AreEqual("none?limit=1", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Simple_Url_WithLimitAndStart_RequestOptions()
        {
            string result = UrlBuilder.FormatRestApiUrl("none", new RequestOptions { Limit = 1, Start = 25 });

            Assert.AreEqual("none?limit=1&start=25", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Simple_Url_WithLimitAndStartAndAt_RequestOptions()
        {
            string result = UrlBuilder.FormatRestApiUrl("none", new RequestOptions { Limit = 1, Start = 25, At = "8" });

            Assert.AreEqual("none?limit=1&start=25&at=8", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Simple_Url_WithStart_RequestOptions_Simple_Param()
        {
            string result = UrlBuilder.FormatRestApiUrl("{0}", new RequestOptions { Start = 1 }, "simple");

            Assert.AreEqual("simple?start=1", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Full_Url_WithStartAndAt_RequestOptions()
        {
            string result = UrlBuilder.FormatRestApiUrl("http://vcs.test.com/{0}", new RequestOptions { Start = 1, At = "25" }, "foo");

            Assert.AreEqual("http://vcs.test.com/foo?start=1&at=25", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Replaceble_Url_WithLimitAndStartAnd_EmptyAt_RequestOptions_Simple_Param()
        {
            string result = UrlBuilder.FormatRestApiUrl("{0}", new RequestOptions { Limit = 1, Start = 25, At = "" }, "some");

            Assert.AreEqual("some?limit=1&start=25", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Complex_Combination()
        {
            string result = UrlBuilder.FormatRestApiUrl("hello/{0}-world/{1}?param1=foo&e=%3D%40%24", new RequestOptions { Limit = 1, Start = 25, At = "6641!%23%25" }, "first", "2nd");

            Assert.AreEqual("hello/first-world/2nd?param1=foo&e=%3D%40%24&limit=1&start=25&at=6641!%23%25", result);
        }

        [TestMethod]
        public void FormatRestApiUrl_Params_Require_UrlEncode()
        {
            string result = UrlBuilder.FormatRestApiUrl("url/{0}?t={1}", null, "f$2%=1+1", "{2nd}");

            Assert.IsTrue(result.Equals("url/f%242%25%3D1%2B1?t=%7B2nd%7D", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
