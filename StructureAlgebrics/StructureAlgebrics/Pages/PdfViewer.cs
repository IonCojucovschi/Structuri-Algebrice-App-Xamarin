using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Joanzapata.Pdfview;

namespace StructureAlgebrics.Pages
{
    [Activity(Label = "Pdf Viewer", MainLauncher = false, Theme = "@style/MyTheme")]
    public class PdfViewer : Activity
    {
        private PDFView pdfView;
        private string resourcePdf;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.pdfView);
            resourcePdf = Intent.Extras.GetString("src");
            pdfView = FindViewById<PDFView>(Resource.Id.pdfView);
            pdfView.FromAsset(resourcePdf).Load();
        }
    }
}