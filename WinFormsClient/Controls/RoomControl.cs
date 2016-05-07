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

namespace WinFormsClient.Controls
{
	public partial class RoomControl : UserControl
	{
		private readonly MainForm mMainForm;

		public RoomControl(MainForm mMainForm)
		{
			this.mMainForm = mMainForm;
			InitializeComponent();
		}

		private async void RoomControl_Load(object sender, EventArgs e)
		{
			mMainForm.Resolver.AddHandler<UserInfoPackage>(UserInfoResived);
			mMainForm.Resolver.AddHandler<RoomInfoPackage>(RoomInfoRecived);
			await mMainForm.Talker.Send(mMainForm.Me, new UserInfoPackage { ID = mMainForm.Me.ID });
		}

		private void RoomInfoRecived(RoomInfoPackage roomInfoPackage)
		{
			mRoomNameLabel.Text = roomInfoPackage.Name;
		}

		private async void UserInfoResived(UserInfoPackage userInfoPackage)
		{
			if (userInfoPackage.ID == mMainForm.Me.ID)
			{
				await mMainForm.Talker.Send(mMainForm.Me, new RoomInfoPackage { ID = userInfoPackage.RoomID });
				return;
			}
		}
	}
}
