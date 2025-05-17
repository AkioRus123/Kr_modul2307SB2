
namespace WpfApp2
{
    internal partial class EditStatusWindow
    {
        private Request selectedRequest;

        public EditStatusWindow(Request selectedRequest)
        {
            this.selectedRequest = selectedRequest;
        }

        public string NewStatus { get; internal set; }

        internal bool ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}