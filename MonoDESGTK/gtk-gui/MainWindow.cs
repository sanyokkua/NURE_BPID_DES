
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.Label labelText;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	
	private global::Gtk.TextView textviewText;
	
	private global::Gtk.Label labelPass;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow3;
	
	private global::Gtk.TextView textviewPass;
	
	private global::Gtk.Button buttonEncrypt;
	
	private global::Gtk.Label labelEncrypt;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	
	private global::Gtk.TextView textviewEncrypted;
	
	private global::Gtk.Button buttonDecrypt;
	
	private global::Gtk.Label labelDecrypt;
	
	private global::Gtk.ScrolledWindow GtkScrolledWindow2;
	
	private global::Gtk.TextView textviewDecrypted;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("DES");
		this.WindowPosition = ((global::Gtk.WindowPosition)(3));
		this.DefaultWidth = 1024;
		this.DefaultHeight = 500;
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.labelText = new global::Gtk.Label ();
		this.labelText.Name = "labelText";
		this.labelText.LabelProp = global::Mono.Unix.Catalog.GetString ("Текст");
		this.vbox1.Add (this.labelText);
		global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.labelText]));
		w1.Position = 0;
		w1.Expand = false;
		w1.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.textviewText = new global::Gtk.TextView ();
		this.textviewText.CanFocus = true;
		this.textviewText.Name = "textviewText";
		this.GtkScrolledWindow.Add (this.textviewText);
		this.vbox1.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow]));
		w3.Position = 1;
		// Container child vbox1.Gtk.Box+BoxChild
		this.labelPass = new global::Gtk.Label ();
		this.labelPass.Name = "labelPass";
		this.labelPass.LabelProp = global::Mono.Unix.Catalog.GetString ("Пароль");
		this.vbox1.Add (this.labelPass);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.labelPass]));
		w4.Position = 2;
		w4.Expand = false;
		w4.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow3 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow3.Name = "GtkScrolledWindow3";
		this.GtkScrolledWindow3.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow3.Gtk.Container+ContainerChild
		this.textviewPass = new global::Gtk.TextView ();
		this.textviewPass.CanFocus = true;
		this.textviewPass.Name = "textviewPass";
		this.GtkScrolledWindow3.Add (this.textviewPass);
		this.vbox1.Add (this.GtkScrolledWindow3);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow3]));
		w6.Position = 3;
		// Container child vbox1.Gtk.Box+BoxChild
		this.buttonEncrypt = new global::Gtk.Button ();
		this.buttonEncrypt.CanFocus = true;
		this.buttonEncrypt.Name = "buttonEncrypt";
		this.buttonEncrypt.UseUnderline = true;
		this.buttonEncrypt.Label = global::Mono.Unix.Catalog.GetString ("Зашифровать");
		this.vbox1.Add (this.buttonEncrypt);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.buttonEncrypt]));
		w7.Position = 4;
		w7.Expand = false;
		w7.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.labelEncrypt = new global::Gtk.Label ();
		this.labelEncrypt.Name = "labelEncrypt";
		this.labelEncrypt.LabelProp = global::Mono.Unix.Catalog.GetString ("Шифротекст");
		this.vbox1.Add (this.labelEncrypt);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.labelEncrypt]));
		w8.Position = 5;
		w8.Expand = false;
		w8.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.textviewEncrypted = new global::Gtk.TextView ();
		this.textviewEncrypted.CanFocus = true;
		this.textviewEncrypted.Name = "textviewEncrypted";
		this.textviewEncrypted.Editable = false;
		this.textviewEncrypted.CursorVisible = false;
		this.GtkScrolledWindow1.Add (this.textviewEncrypted);
		this.vbox1.Add (this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow1]));
		w10.Position = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.buttonDecrypt = new global::Gtk.Button ();
		this.buttonDecrypt.CanFocus = true;
		this.buttonDecrypt.Name = "buttonDecrypt";
		this.buttonDecrypt.UseUnderline = true;
		this.buttonDecrypt.Label = global::Mono.Unix.Catalog.GetString ("Расшифровать");
		this.vbox1.Add (this.buttonDecrypt);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.buttonDecrypt]));
		w11.Position = 7;
		w11.Expand = false;
		w11.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.labelDecrypt = new global::Gtk.Label ();
		this.labelDecrypt.Name = "labelDecrypt";
		this.labelDecrypt.LabelProp = global::Mono.Unix.Catalog.GetString ("Расшифрованный текст");
		this.vbox1.Add (this.labelDecrypt);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.labelDecrypt]));
		w12.Position = 8;
		w12.Expand = false;
		w12.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow2 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow2.Name = "GtkScrolledWindow2";
		this.GtkScrolledWindow2.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow2.Gtk.Container+ContainerChild
		this.textviewDecrypted = new global::Gtk.TextView ();
		this.textviewDecrypted.CanFocus = true;
		this.textviewDecrypted.Name = "textviewDecrypted";
		this.textviewDecrypted.Editable = false;
		this.textviewDecrypted.CursorVisible = false;
		this.GtkScrolledWindow2.Add (this.textviewDecrypted);
		this.vbox1.Add (this.GtkScrolledWindow2);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.GtkScrolledWindow2]));
		w14.Position = 9;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.buttonEncrypt.Clicked += new global::System.EventHandler (this.OnButtonEncryptClicked);
		this.buttonDecrypt.Clicked += new global::System.EventHandler (this.OnButtonDecryptClicked);
	}
}