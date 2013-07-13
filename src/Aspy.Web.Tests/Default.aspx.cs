using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;

namespace Aspy.Web.Tests
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void _sessionSimpleValue_Click(object sender, EventArgs e)
        {
            _sessionSimpleValueLabel.Visible = true;
            AddToSession("Some simple string value string");
        }

        protected void _sessionDictionary_Click(object sender, EventArgs e)
        {
            _sessionDictionaryLabel.Visible = true;
            AddToSession(CreateDictionary());
        }

        protected void _sessionComplexObject_Click(object sender, EventArgs e)
        {
            _sessionComplexObjectLabel.Visible = true;
            AddToSession(CreateComplexObject());
        }

        protected void _cacheSimpleValue_Click(object sender, EventArgs e)
        {
            _cacheSimpleValueLabel.Visible = true;
            AddToCache("Some simple string value string");
        }

        protected void _cacheDictionary_Click(object sender, EventArgs e)
        {
            _cacheDictionaryLabel.Visible = true;
            AddToCache(CreateDictionary());
        }

        protected void _cacheComplexObject_Click(object sender, EventArgs e)
        {
            _cacheComplexObjectLabel.Visible = true;
            AddToCache(CreateComplexObject());
        }

        private void AddToSession(object value)
        {
            for (var i = 1; i <= int.MaxValue; i++)
            {
                var key = "Session Key " + i;
                if (Session.Keys.Cast<string>().All(x => x != key))
                {
                    Session.Add(key, value);
                    return;
                }
            }
        }

        private void AddToCache(object value)
        {
            for (var i = 1; i <= int.MaxValue; i++)
            {
                var key = "Cache Key " + i;
                if (Cache.Cast<DictionaryEntry>().All(x => !x.Key.Equals(key)))
                {
                    Cache.Add(key, value, null, DateTime.Now.AddDays(1), TimeSpan.Zero, CacheItemPriority.Default, null);
                    return;
                }
            }
        }

        private object CreateComplexObject()
        {
            return typeof (object);
        }

        private object CreateDictionary()
        {
            return new Dictionary<string, object>
                       {
                           {"Key 1", 123.0f},
                           {"Key 2", new object()},
                           {"Key 3", "Some string value"},
                           {"Key 4", new ComplexClass()}
                       };
        }
    }
}