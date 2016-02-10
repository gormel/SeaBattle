using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;

namespace WinFormsClient
{
	public partial class Form1 : Form
	{
		private Socket mSocket;
		private User mMe;
		private ClientTalker mTalker = new ClientTalker();
		private PackageResolver mResolver = new PackageResolver();

		public Form1()
		{
			InitializeComponent();
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			mSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
			mMe = new User(mSocket);
			mResolver.OnUpdateUserList += mResolver_OnUpdateUserList;
			await Connect();
		}

		void mResolver_OnUpdateUserList(object sender, List<string> e)
		{
			listBox1.Items.Clear();
			listBox1.Items.AddRange(e.Cast<object>().ToArray());
		}

		private async Task Connect()
		{
			await mSocket.ConnectTaskAsync(await Dns.GetHostAddressesAsync("localhost"), 666);
			await mTalker.StartListen(mMe, p =>
			{
				BeginInvoke(new Action(() => mResolver.ResolvePackage(p)));
			});
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			try
			{
				button1.Enabled = false;
				await mTalker.Send(mMe, new GetUserList());
			}
			finally
			{
				button1.Enabled = true;
			}
		}
	}
}
