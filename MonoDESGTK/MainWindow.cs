using System;
using Gtk;
using MonoDESGTK;

public partial class MainWindow: Gtk.Window
{
	private DES des;
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		Build ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButtonEncryptClicked (object sender, EventArgs e)
	{
		textviewEncrypted.Buffer.Text = "";
		textviewDecrypted.Buffer.Text = "";
		if (textviewText.Buffer.Text.Length > 0 && textviewPass.Buffer.Text.Length == 16) {
			EncryptText ();
		}
	}

	void EncryptText ()
	{
		des = new DES ();
		string text = textviewText.Buffer.Text;
		string pass = textviewPass.Buffer.Text;
		byte[] byteText = GeneralMethods.ConvertStringToByteArray (text);
		byte[] bytePass = GeneralMethods.ConvertStringToByteArray (pass);
		byte[] result = des.Encrypt (byteText, bytePass);
		string encrypted = GeneralMethods.ConvertByteArrayToString (result);
		textviewEncrypted.Buffer.Text = encrypted;
	}

	protected void OnButtonDecryptClicked (object sender, EventArgs e)
	{
		if (textviewEncrypted.Buffer.Text.Length > 0) {
			Decrypt ();
		}
	}

	void Decrypt ()
	{
		des = new DES ();
		string encryptedText = textviewEncrypted.Buffer.Text;
		string pass = textviewPass.Buffer.Text;
		byte[] byteEncrypted = GeneralMethods.ConvertStringToByteArray (encryptedText);
		byte[] bytePass = GeneralMethods.ConvertStringToByteArray (pass);
		byte[] result = des.Decrypt (byteEncrypted, bytePass);
		string decrypted = GeneralMethods.ConvertByteArrayToString (result);
		textviewDecrypted.Buffer.Text = decrypted;

	}
}
