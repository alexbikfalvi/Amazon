using System;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using DotNetApi.Security;
using DotNetApi.Web;
using DotNetApi.Windows;
using AwsApi;
using AwsApi.Atom;
using AwsApi.Atom.Alexa;

namespace AwsApiTest
{
	public partial class FormMain : Form
	{
		private AwsAlexaRequest request = new AwsAlexaRequest();
		private IAsyncResult result = null;

		public FormMain()
		{
			InitializeComponent();
		}

		private void OnStart(object sender, EventArgs e)
		{
			this.buttonStart.Enabled = false;
			this.buttonStop.Enabled = true;
			// Read to registry.

			byte[] cryptoKey = Registry.GetBytes("HKEY_CURRENT_USER\\Software\\Alex Bikfalvi\\Amazon", "CryptoKey", null);
			byte[] cryptoIV = Registry.GetBytes("HKEY_CURRENT_USER\\Software\\Alex Bikfalvi\\Amazon", "CryptoIV", null);
			SecureString accessKey = Registry.GetSecureString("HKEY_CURRENT_USER\\Software\\Alex Bikfalvi\\Amazon", "AccessKey", SecureStringExtensions.Empty, cryptoKey, cryptoIV);
			SecureString secretKey = Registry.GetSecureString("HKEY_CURRENT_USER\\Software\\Alex Bikfalvi\\Amazon", "SecretKey", SecureStringExtensions.Empty, cryptoKey, cryptoIV);

			this.textBox.AppendText(string.Format("[{0}] Access key: {1}\r\n", DateTime.Now, accessKey.ConvertToUnsecureString()));
			this.textBox.AppendText(string.Format("[{0}] Secret key: {1}\r\n", DateTime.Now, secretKey.ConvertToUnsecureString()));

			this.textBox.AppendText(string.Format("[{0}] Started request.\r\n", DateTime.Now));

			this.result = this.request.Begin(
				accessKey,
				secretKey,
				null,
				null,
				1,
				100,
				AwsAlexaRequest.ResponseGroup.Country,
				this.OnCallback,
				null);

			AsyncWebResult result = (AsyncWebResult)this.result;

			this.textBox.AppendText(string.Format("[{0}] Request URL is: {1}\r\n", DateTime.Now, result.Request.RequestUri));
		}

		private void OnStop(object sender, EventArgs e)
		{
			this.textBox.AppendText(string.Format("[{0}] Canceling request.\n", DateTime.Now));

			this.buttonStop.Enabled = false;

			this.request.Cancel(this.result);
		}

		private void OnCallback(AsyncWebResult result)
		{
			// Call this on the UI thread.
			if (this.InvokeRequired)
			{
				this.Invoke(new AsyncWebRequestCallback(this.OnCallback), new object[] { result });
				return;
			}

			this.buttonStart.Enabled = true;
			this.buttonStop.Enabled = false;

			try
			{
				AtomAtsTopSitesResponse response = this.request.End(result);

				this.textBox.AppendText(string.Format("[{0}] Finished request: success.\r\n", DateTime.Now));

				// Show the response.
				this.textBox.AppendText(result.ReceiveData.ToString());
			}
			catch (AwsException exception)
			{
				this.textBox.AppendText(string.Format("[{0}] Finished request: AWS exception. {1}\r\n", DateTime.Now, exception.Message));
				if (exception.XmlResponse != null)
				{
					this.textBox.AppendText(string.Format("\tResponse ID: {0}\r\n\tCode: {1}\r\n\tMessage: {2}",
						exception.XmlResponse.RequestId,
						exception.XmlResponse.Errors[0].Code,
						exception.XmlResponse.Errors[0].Message));
				}
			}
			catch (Exception exception)
			{
				this.textBox.AppendText(string.Format("[{0}] Finished request: exception. {1}\r\n", DateTime.Now, exception.Message));
			}
		}

		private void OnLoadFile(object sender, EventArgs e)
		{
			if (this.openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				this.textBox.AppendText(string.Format("[{0}] Parsing the XML file \'{1}\'.\r\n", DateTime.Now, this.openFileDialog.FileName));
				try
				{
					// Open the XML document from file.
					XDocument xml = XDocument.Load(this.openFileDialog.FileName);
					// Parse the document into an atom object.
					AtomAtsTopSitesResponse response = AtomAtsTopSitesResponse.Parse(xml.Root);
					// Display message.
					this.textBox.AppendText(string.Format("[{0}] Done.\r\n", DateTime.Now));
				}
				catch (Exception exception)
				{
					this.textBox.AppendText(string.Format("[{0}] Load file: exception. {1}\r\n", DateTime.Now, exception.Message));
				}
			}
		}
	}
}
