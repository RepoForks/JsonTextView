﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SPControls
{
    public partial class JsonTextView : Label
    {
        public static readonly BindableProperty JsonProperty = BindableProperty.Create("Json", typeof(string), typeof(JsonTextView), propertyChanged: (bindable, oldVal, newVal) =>
        {
            var view = (JsonTextView)bindable;
            view.SetJson((string)newVal);
        });
        public string Json
        {
            get { return (string)GetValue(JsonProperty); }
            set { SetValue(JsonProperty, value); }
        }
        public JsonTextView()
        {
            InitializeComponent();
            FormattedText = new FormattedString();
        }

        public void SetJson(string json)
        {
            try
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                foreach (var pair in dict)
                {
                    var boldSpan = new Span()
                    {
                        Text = $"{pair.Key}: ",
                        FontAttributes = FontAttributes.Bold
                    };
                    var detailSpan = new Span()
                    {
                        Text = pair.Value
                    };
                    var lineBreak = new Span()
                    {
                        Text = "\n"
                    };
                    FormattedText.Spans.Add(boldSpan);
                    FormattedText.Spans.Add(detailSpan);
                    FormattedText.Spans.Add(lineBreak);
                }
            }
            catch
            {
                Debug.WriteLine("Failed to parse json.");
            }
        }
    }
}