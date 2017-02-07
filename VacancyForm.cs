using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalRU
{
    public partial class VacancyForm : Form
    {
        ReqBlockList RBList;
        SRL.Vacancy defaultVacancy;
        SRL.SRL srl;

        public bool isOKed = false;
        public bool isChange = false;
        public bool isPublic = false;

        public VacancyForm(SRL.Vacancy defaultVacancy, SRL.SRL srl, bool isChange)
        {
            InitializeComponent();
            this.isChange = isChange;
            this.srl = srl;
            this.defaultVacancy = defaultVacancy;

            RBList = new ReqBlockList(this, this.panel1, new Point(25, 25));
            initRBList(this.defaultVacancy);
        }
        private void initRBList(SRL.Vacancy vac)
        {
            RBList.createBlock("Наименование должности:", false);
            RBList.addItemToBlock(vac.JobTitle, "Наименование должности:");
            RBList.createBlock("Требования к образованию:",true);
            foreach (SRL.Item item in vac.EducationRequirements.ItList)
                RBList.addItemToBlock(item.item_str, "Требования к образованию:");
            RBList.createBlock("Требования к стажу работы:",false);
                RBList.addItemToBlock(vac.WorkExperience, "Требования к стажу работы:");
            RBList.createBlock("Описание трудовой деятельности:", true);
            foreach (SRL.Item item in vac.DescriptionOfWork.ItList)
                RBList.addItemToBlock(item.item_str, "Описание трудовой деятельности:");
            RBList.createBlock("Место работы и контакты:",false);
            RBList.addItemToBlock(vac.Location, "Место работы и контакты:");
            RBList.createBlock("Режим работы:", true);
            foreach (SRL.Item item in vac.TimeTable.ItList)
                RBList.addItemToBlock(item.item_str, "Режим работы:");
            RBList.createBlock("Уровень зарплаты:", false);
            RBList.addItemToBlock(vac.WageLevel, "Уровень зарплаты:");
            RBList.createBlock("Требования к знанию языка:",true);
            foreach (SRL.Item item in vac.LanguageRequirements.ItList)
                RBList.addItemToBlock(item.item_str, "Требования к знанию языка:");
            RBList.createBlock("Требования к гражданству:",true);
            foreach (SRL.Item item in vac.CitizenshipRequirements.ItList)
                RBList.addItemToBlock(item.item_str, "Требования к гражданству:");
            RBList.createBlock("Участок производства:",false);
            RBList.addItemToBlock(vac.ProductionSite, "Участок производства:");
            RBList.refresh();
            
        }
        public SRL.Vacancy get_vacancy() {
            SRL.Vacancy vac = new SRL.Vacancy();
            if (!this.isChange)
            {
                RequirBlock JT_RBlock = this.RBList.getRBlockByTitle("Наименование должности:");
                vac.JobTitle = JT_RBlock.getItem(0).getText();
                RequirBlock ER_RBlock = this.RBList.getRBlockByTitle("Требования к образованию:");
                foreach (Item item in ER_RBlock.getList())
                    vac.EducationRequirements.Add(item.getText());
                RequirBlock WE_RBlock = this.RBList.getRBlockByTitle("Требования к стажу работы:");
                vac.WorkExperience = WE_RBlock.getItem(0).getText();
                RequirBlock DOW_RBlock = this.RBList.getRBlockByTitle("Описание трудовой деятельности:");
                foreach (Item item in DOW_RBlock.getList())
                    vac.DescriptionOfWork.Add(item.getText());
                RequirBlock Location_RBlock = this.RBList.getRBlockByTitle("Место работы и контакты:");
                vac.Location = Location_RBlock.getItem(0).getText();
                RequirBlock TT_RBlock = this.RBList.getRBlockByTitle("Режим работы:");
                foreach (Item item in TT_RBlock.getList())
                    vac.TimeTable.Add(item.getText());
                RequirBlock WL_RBlock = this.RBList.getRBlockByTitle("Уровень зарплаты:");
                vac.WageLevel = WL_RBlock.getItem(0).getText();
                RequirBlock LR_RBlock = this.RBList.getRBlockByTitle("Требования к знанию языка:");
                foreach (Item item in LR_RBlock.getList())
                    vac.LanguageRequirements.Add(item.getText());
                RequirBlock CR_RBlock = this.RBList.getRBlockByTitle("Требования к гражданству:");
                foreach (Item item in CR_RBlock.getList())
                    vac.CitizenshipRequirements.Add(item.getText());
                RequirBlock PS_RBlock = this.RBList.getRBlockByTitle("Участок производства:");
                vac.ProductionSite = PS_RBlock.getItem(0).getText();
                
                vac.ID = this.srl.publicVacancy(vac);
            }
            else
            {
                RequirBlock JT_RBlock = this.RBList.getRBlockByTitle("Наименование должности:");
                vac.JobTitle = JT_RBlock.getItem(0).getText();
                RequirBlock ER_RBlock = this.RBList.getRBlockByTitle("Требования к образованию:");
                foreach (Item item in ER_RBlock.getList())
                    vac.EducationRequirements.Add(item.getText());
                RequirBlock WE_RBlock = this.RBList.getRBlockByTitle("Требования к стажу работы:");
                vac.WorkExperience = WE_RBlock.getItem(0).getText();
                RequirBlock DOW_RBlock = this.RBList.getRBlockByTitle("Описание трудовой деятельности:");
                foreach (Item item in DOW_RBlock.getList())
                    vac.DescriptionOfWork.Add(item.getText());
                RequirBlock Location_RBlock = this.RBList.getRBlockByTitle("Место работы и контакты:");
                vac.Location = Location_RBlock.getItem(0).getText();
                RequirBlock TT_RBlock = this.RBList.getRBlockByTitle("Режим работы:");
                foreach (Item item in TT_RBlock.getList())
                    vac.TimeTable.Add(item.getText());
                RequirBlock WL_RBlock = this.RBList.getRBlockByTitle("Уровень зарплаты:");
                vac.WageLevel = WL_RBlock.getItem(0).getText();
                RequirBlock LR_RBlock = this.RBList.getRBlockByTitle("Требования к знанию языка:");
                foreach (Item item in LR_RBlock.getList())
                    vac.LanguageRequirements.Add(item.getText());
                RequirBlock CR_RBlock = this.RBList.getRBlockByTitle("Требования к гражданству:");
                foreach (Item item in CR_RBlock.getList())
                    vac.CitizenshipRequirements.Add(item.getText());
                RequirBlock PS_RBlock = this.RBList.getRBlockByTitle("Участок производства:");
                vac.ProductionSite = PS_RBlock.getItem(0).getText();

                vac.ResponsesCount = defaultVacancy.ResponsesCount;
                vac.ID = defaultVacancy.ID;
            }
            vac.UpdatedDate = defaultVacancy.UpdatedDate;
            vac.isShown = defaultVacancy.isShown;
            vac.basedUnitID = defaultVacancy.basedUnitID;
            vac.LID = this.defaultVacancy.LID;
            return vac; 
        }
        private void panel1_Click(object sender, EventArgs e){this.panel1.Focus();}

        private void okButt_Click(object sender, EventArgs e)
        {
            this.isOKed = true;
            this.isPublic = this.checkBox1.Checked;
            this.Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.isOKed = false;
            this.Close();
        }
    }
}
