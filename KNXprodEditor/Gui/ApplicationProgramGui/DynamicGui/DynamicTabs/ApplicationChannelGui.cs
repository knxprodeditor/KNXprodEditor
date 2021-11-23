using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KnxProd.Model;

namespace knxprod_ns
{
    public partial class ApplicationChannelGui : UserControl
    {
        private static ApplicationChannelGui mApplicationChannelGui;

        public ApplicationChannelGui()
        {
            InitializeComponent();
            mApplicationChannelGui = this;
        }

        /***********************************************************************************************************************/
        // ApplicationChannel View

        public static void FillApplicationProgramChannelPage(ApplicationProgramChannel_t appProgCh)
        {
            mApplicationChannelGui.textAcId.Text = appProgCh.Id;
            mApplicationChannelGui.textAcName.Text = appProgCh.Name;
            mApplicationChannelGui.textAcText.Text = appProgCh.Text;
            mApplicationChannelGui.textAcNumber.Text = appProgCh.Number;
            mApplicationChannelGui.textAcTextParameterRefId.Text = appProgCh.TextParameterRefId;
            mApplicationChannelGui.textAcInternalDescription.Text = appProgCh.InternalDescription;
            mApplicationChannelGui.textAcIcon.Text = appProgCh.Icon;
            mApplicationChannelGui.textAcHelpContext.Text = appProgCh.HelpContext;
            mApplicationChannelGui.textAcSemantics.Text = appProgCh.Semantics;
        }

        /***********************************************************************************************************************/
        // ApplicationChannel Edit


        // speichern button im ApplicationChannel Tab
        private void buttonApplicationChannelSave_Click(object sender, EventArgs e)
        {
            ApplicationProgramChannel_t newApplicationProgramChannel = new ApplicationProgramChannel_t();
            bool newItem = false;

            // ein bestehender ApplicationProgramChannel Eintrag wurde verändert
            if ((DynamicTreeViewGui.GetSelectedTreeNode().Tag is ApplicationProgramChannel_t) && (DynamicTreeViewGui.GetSelectedTreeNode().Tag as ApplicationProgramChannel_t).Id == textAcId.Text)
            {
                newApplicationProgramChannel = DynamicTreeViewGui.GetSelectedTreeNode().Tag as ApplicationProgramChannel_t;
            }
            else // ein neuer ApplicationProgramChannel Eintrag wurde erzeugt
            {
                newItem = true;
                int newAppChannelId = FindApplicationProgramChannelIds() + 1;

                // der neue ApplicationProgramChannel wird angehängt
                DynamicSectionEdit.AddToDynamicObject(DynamicTreeViewGui.GetSelectedTreeNode().Tag, newApplicationProgramChannel);

                newApplicationProgramChannel.Id = ApplicationProgramGui.selectedApplicationProgram.Id + "_CH-" + newAppChannelId.ToString();
                newApplicationProgramChannel.Number = newAppChannelId.ToString();
                newApplicationProgramChannel.Items = new object[0];
            }

            newApplicationProgramChannel.Name = Extensions.NullIfEmpty(textAcName.Text);
            newApplicationProgramChannel.Text = Extensions.NullIfEmpty(textAcText.Text);
            newApplicationProgramChannel.TextParameterRefId = Extensions.NullIfEmpty(textAcTextParameterRefId.Text);
            newApplicationProgramChannel.InternalDescription = Extensions.NullIfEmpty(textAcInternalDescription.Text);
            newApplicationProgramChannel.Icon = Extensions.NullIfEmpty(textAcIcon.Text);
            newApplicationProgramChannel.HelpContext = Extensions.NullIfEmpty(textAcHelpContext.Text);
            newApplicationProgramChannel.Semantics = Extensions.NullIfEmpty(textAcSemantics.Text);

            if (newItem)
            {
                TreeNode channelChild = new TreeNode();
                channelChild = DynamicTreeViewGui.GetSelectedTreeNode().Nodes.Add("Channel ID: " + newApplicationProgramChannel.Id); //Beschreibung des Parameterblocks
                channelChild.Tag = newApplicationProgramChannel;
                channelChild.ImageIndex = (int)Images.channel;
                channelChild.SelectedImageIndex = (int)Images.channel;

                DynamicTreeViewGui.SetSelectedTreeNode(channelChild);
            }
        }





        /// <summary>
        /// sucht die höchste bisher verwendete ApplicationChannel Id
        /// </summary>
        /// <returns>die höchste bisher verwendete ApplicationChannel Id</returns>
        public static int FindApplicationProgramChannelIds()
        {
            List<string> usedAppProgChannelIds = new List<string>();
            List<int> usedAppProgChannelIdNumbers = new List<int>();
            foreach (var dynChannel in ApplicationProgramGui.selectedApplicationProgram.Dynamic) //channels
            {
                if (dynChannel is ApplicationProgramChannel_t)
                {
                    usedAppProgChannelIds.Add((dynChannel as ApplicationProgramChannel_t).Id);
                }
            }

            foreach (string usedAppProgChannelId in usedAppProgChannelIds)
            {
                usedAppProgChannelIdNumbers.Add(int.Parse(usedAppProgChannelId.Remove(0, ApplicationProgramGui.selectedApplicationProgram.Id.Length + 4)));
            }
            usedAppProgChannelIdNumbers.Sort();
            int highestUsedAppProgChannelIdNumber = 0;
            if (usedAppProgChannelIdNumbers.Count > 0)
            {
                highestUsedAppProgChannelIdNumber = usedAppProgChannelIdNumbers[usedAppProgChannelIdNumbers.Count - 1];
            }

            return highestUsedAppProgChannelIdNumber;
        }

        // bereitet den ApplicationChannel Tab für die Eintragung neuer Daten vor
        public static void ClearAplicationChannelTab()
        {
            mApplicationChannelGui.textAcId.Text = "";
            mApplicationChannelGui.textAcName.Text = "";
            mApplicationChannelGui.textAcText.Text = "";
            mApplicationChannelGui.textAcNumber.Text = "";
            mApplicationChannelGui.textAcTextParameterRefId.Text = "";
            mApplicationChannelGui.textAcInternalDescription.Text = "";
            mApplicationChannelGui.textAcIcon.Text = "";
            mApplicationChannelGui. textAcHelpContext.Text = "";
            mApplicationChannelGui.textAcSemantics.Text = "";
        }
    }
}
