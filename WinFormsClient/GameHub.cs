using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core.Packeges;

namespace WinFormsClient
{
	public partial class GameHub : UserControl
	{
		private readonly MainForm mMainForm;

		public GameHub(MainForm mainForm)
		{
			this.mMainForm = mainForm;
			InitializeComponent();
		}

		private async void GameHub_Load(object sender, EventArgs e)
		{
			mMainForm.Resolver.AddHandler<RoomList>(RoomListRecived);
			mMainForm.Resolver.AddHandler<RoomInfoPackage>(RoomInfoRecived);
			RefreshRooms();
		}

		private void RoomListRecived(RoomList list)
		{
			foreach (var room in list.Rooms)
			{
				mMainForm.Talker.Send(mMainForm.Me, new RoomInfoPackage
				{
					ID = room
				});
			}
		}

		private void RefreshRooms()
		{
			mRoomsListBox.Items.Clear();
			mMainForm.Talker.Send(mMainForm.Me, new RoomList());
		}

		private void RoomInfoRecived(RoomInfoPackage info)
		{
			mRoomsListBox.Items.Add(new { info.ID, info.Name, info.UserCount });
		}

		private void mRefreshButton_Click(object sender, EventArgs e)
		{
			RefreshRooms();
		}
	}
}
