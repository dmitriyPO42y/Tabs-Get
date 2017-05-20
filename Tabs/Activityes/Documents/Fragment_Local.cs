using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Joanzapata.Pdfview;

namespace Tabs.Activityes.Documents
{
    public class Fragment_Local : Android.Support.V4.App.Fragment
    {
        public string fileName { get; set; } = null;

        private PDFView pdfView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.localDocuments_fragment, container, false);

            if (!string.IsNullOrEmpty(fileName))
            {
                pdfView = view.FindViewById<PDFView>(Resource.Id.pdfView);
                pdfView.FromAsset(fileName).DefaultPage(10).SwipeVertical(true).EnableDoubletap(true).Load();
            }

            return view;
        }
    }
}