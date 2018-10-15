using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.Repository
{
    public interface ILoanPurposeDetails
    { 
        By BtnHideShowDebug { get; }

        By DonePurposeLoanDialog { get; }

        By Slider { get; }

        By SliderHandle { get; }

        By SliderTrack { get; }
//used
        By FirstLoanAmount { get; }
//used
        By FourthLoanAmount { get; }
//used
        By ThirdLoanAmount { get; }
//used
        By FifthLoanAmount { get; }

        By SelectSecondPurpose { get; }
//used
        By SecondLoanAmount { get; }

        By ContinueButton { get; }

        By PurposeLoanDialog { get; }

        By SliderMaxBtn { get; }

        By SliderMinBtn { get; }

        By SelectFirstPurpose { get; }

        By BtnAddAnotherpurpose { get; }       

        By LoanWarningmsg { get; }

        By ContinueButtonRL { get; }

        By EditMyDetailsBtn { get; }

        By PersonalDetailsHeader { get; }

        By FirstName { get; }

        By PersonalDetailsEditDoneBtn { get; }

        By BackBtn { get; }

        By MoreInfoPOPUP { get; }

        By MoreInfoBackBtn { get; }

        By MoreInformationTextArea { get; }

        By MoreInformationContinue { get; }

        By CancelPOL { get; }

        By AnythingElseText { get; }

        By ErrorMessageSamePOL { get; }

        By cancelPurposeLoanDialog { get; }
        
    }

    public class LoanPurposeDetailsMobileNLLoc : ILoanPurposeDetails
    {
        public By BtnHideShowDebug => By.XPath(".//*[@id='debug-show']");

        public By PurposeLoanDialog => By.XPath(".//*[@id='POLDialog']");
       // public By PurposeLoanDialog => By.XPath(".//div[@class='rc-dialog polDialog']");

         public By DonePurposeLoanDialog => By.XPath(".//*[@id='purpose-done-btn2']");
      //  public By DonePurposeLoanDialog => By.XPath(".//div[@class='polButtons dialog-buttons single-button']/input");

        public By Slider => By.XPath(".//div[@id='apply-slider_wrap']//span");
       // public By Slider => By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']");

        // public By SliderHandle => By.XPath(".//*[@id='apply-slider_wrap']/div[1]/span");

        //public By SliderTrack => By.XPath(".//*[@id='apply-slider']");


        public By SliderHandle => By.XPath(".//div[@class='display-value white text-center bold600']/span"); 

        public By SliderTrack => By.XPath(".//div[@id='apply-slider']"); //new

        public By FirstLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[1]//div[@class='selPolSection-amountContainer']/input");

       // public By FirstLoanAmount => By.XPath(".//*[@class='purposes']/div[1]//input");

        //public By SelectFirstPurpose => By.XPath(".//*[@class='purposes']/div[1]//span[contains(text(),'- select purpose -')]");

        public By SelectFirstPurpose => By.XPath("(//span[text()='- select purpose -'])[last()-2]");

       // public By SelectSecondPurpose => By.XPath(".//*[@class='purposes']/div[2]//span[contains(text(),'- select purpose -')]");

         public By SelectSecondPurpose => By.XPath("(.//div[@class='dialog-select field']/span)[last()]");     

       /// public By SecondLoanAmount => By.XPath(".//*[@class='purposes']/div[2]//input");

        public By SecondLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[2]//div[@class='selPolSection-amountContainer']/input");

        public By BtnAddAnotherpurpose => By.XPath(".//button[@class='button green']");

        //public By ThirdLoanAmount => By.XPath(".//*[@id='selPolSectionAmount-6']");

        public By ThirdLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[3]//div[@class='selPolSection-amountContainer']/input");//new

        public By FourthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[4]//div[@class='selPolSection-amountContainer']/input");//new

        public By FifthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[5]//div[@class='selPolSection-amountContainer']/input");//new

        // public By ContinueButton => By.XPath(".//*[@class='button'][contains(text(),'Continue')]");
        public By ContinueButton => By.XPath(".//*[@id='next1']");

        public By ContinueButtonRL => By.XPath(".//*[@id='btnPOLsCompleted']");

        public By cancelPurposeLoanDialog => By.XPath(".//button[@id='cancelPolDlg']");

        public By SliderMaxBtn => By.XPath(".//div[@class='isButton ui-btn-c maxAmount plusMinusBtn f-right font-14 charcoal']");

       // public By SliderMaxBtn => By.XPath(".//div[@class='sliderButtons']/button[contains(text(),'+')]");

        public By SliderMinBtn => By.XPath(".//div[@class='isButton ui-btn-c minAmount plusMinusBtn f-left font-14 charcoal']");

       // public By SliderMinBtn => By.XPath(".//div[@class='sliderButtons']/button[contains(text(),'-')]");

        public By LoanWarningmsg => By.XPath(".//div[@class='error-arrow']");

        public By MoreInfoPOPUP => By.XPath(".//div[@id='poloverallmoreinfo-container']");

        public By MoreInfouserinput => By.XPath(".//div[@class='rc-dialog moreInfoDialog']//div[@class='form-fields']/textarea");

        public By MoreInfoContinueBtn => By.XPath(".//div[@class='dialog-buttons']/input[@value='Continue']");

        public By MoreInfoBackBtn => By.XPath(".//div[@class='dialog-buttons']/input[@value='Back']");

        public By MoreInformationTextArea => By.XPath(".//textarea[@id='poloverallmoreinfo']");

        public By MoreInformationContinue => By.XPath(".//div[@class='popup-buttons buttons-two']/a[contains(text(),'Continue')]");

        public By CancelPOL => By.XPath("//*[@id='cancelPolDlg']");

        public By AnythingElseText => By.XPath("//input[@class='polMoreDetails']");

        public By ErrorMessageSamePOL => By.XPath("//div[@class='error-wrap']/p");

        public By EditMyDetailsBtn => By.XPath(".//button[@id='btnEditDetails']");

        public By PersonalDetailsHeader => By.XPath(".//div[@id='mobile-confirmdetails-details']//a[@data-page='personal']");

        public By FirstName => By.XPath(".//input[@id='firstname']");

        public By PersonalDetailsEditDoneBtn => By.XPath(".//button[@id='submit-1']");

        public By BackBtn => By.XPath(".//a[@id='btnback']");
                
    }

    public class LoanPurposeDetailsDesktopNLLoc : ILoanPurposeDetails
    {
        public By BtnHideShowDebug => By.XPath(".//*[@id='debug-show']");

        public By PurposeLoanDialog => By.XPath(".//*[@id='POLDialog']");

        // public By PurposeLoanDialog => By.XPath(".//div[@class='rc-dialog polDialog']");

        //public By DonePurposeLoanDialog => By.XPath(".//*[@id='purpose-done-btn2']");

        public By DonePurposeLoanDialog => By.XPath(".//*[@class='polButtons dialog-buttons single-button']/input");

        //public By Slider => By.XPath(".//*[@class='rc-slider-handle']");

        //public By DonePurposeLoanDialog => By.XPath(".//*[@class='polButtons dialog-buttons single-button']/input");

        public By Slider => By.XPath(".//div[@class='rc-slider-handle']");  

        // public By Slider => By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']");

        public By SliderHandle => By.XPath(".//div[@class='slider-tooltip-inner']");

        //public By SliderHandle => By.XPath(".//*[@id='apply-slider_wrap']/div[1]/span");

        public By SliderTrack => By.XPath(".//div[@class='rc-slider-step']");

        //public By SliderTrack => By.XPath(".//*[@id='apply-slider']");

        //public By FirstLoanAmount => By.XPath(".//*[@class='purposes']/div[1]//input");

        //public By FirstLoanAmount => By.XPath("(.//*[@class='font-16 inline-block border radius-5'])[last()-2]");

        public By FirstLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[1]//div[@class='selPolSection-amountContainer']/input");

        public By SelectFirstPurpose => By.XPath("(//span[text()='- select purpose -'])[last()-1]");//new

       //public By SelectSecondPurpose => By.XPath(".//*[@id='selPolSections-container']/div[2]//span[contains(text(),'- select purpose -')]");
        public By SelectSecondPurpose => By.XPath("(.//div[@class='dialog-select field']/span)[last()]");//new

        //public By SecondLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[2]//div[@class='selPolSection-amountContainer']/input");
        public By SecondLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[2]//div[@class='selPolSection-amountContainer']/input");

        public By ThirdLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[3]//div[@class='selPolSection-amountContainer']/input");//new

        public By FourthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[4]//div[@class='selPolSection-amountContainer']/input");//new

        public By FifthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[5]//div[@class='selPolSection-amountContainer']/input");//new

        public By BtnAddAnotherpurpose => By.XPath(".//button[@class='button green']");

        // public By ThirdLoanAmount => By.XPath(".//*[@id='selPolSectionAmount-6']");
       // public By ThirdLoanAmount => By.XPath(".//*[@class='purposes']/div[3]//input");//new

        //public By ContinueButton => By.XPath(".//*[@id='btnPOLsCompleted']");
         public By ContinueButton => By.XPath(".//*[@class='button'][contains(text(),'Continue')]");

        public By ContinueButtonRL => By.XPath(".//*[@id='btnPOLsCompleted']");

        public By cancelPurposeLoanDialog => By.XPath(".//button[@id='cancelPolDlg']");

        public By LoanWarningmsg => By.XPath(".//div[@class='error-arrow']");

        public By MoreInfoPOPUP => By.XPath(".//div[@class='rc-dialog moreInfoDialog']");

        public By MoreInfouserinput => By.XPath(".//div[@class='rc-dialog moreInfoDialog']//div[@class='form-fields']/textarea");

        public By MoreInfoContinueBtn => By.XPath(".//div[@class='dialog-buttons']/input[@value='Continue']");

        public By MoreInfoBackBtn => By.XPath(".//div[@class='dialog-buttons']/input[@value='Back']");

        public By MoreInformationTextArea => By.XPath("//*[@name='poloverallmoreinfo']");

        public By MoreInformationContinue => By.XPath("//*[@value='Continue']");

        public By CancelPOL => By.XPath("//*[@id='cancelPolDlg']");

        public By AnythingElseText => By.XPath("//*[@placeholder='Specific details are helpful']");

        public By ErrorMessageSamePOL => By.XPath("//div[@class='error-wrap']/p");

        public By EditMyDetailsBtn => By.XPath(".//a[@id='editDetailsBtn']");

        public By PersonalDetailsHeader => By.XPath(".//div[@id='detailsAccordion']/h3[@id='personalDetailsHeader']");

        public By FirstName => By.XPath(".//input[@id='firstname']");

        public By PersonalDetailsEditDoneBtn => By.XPath(".//button[@id='submit-1']");

        public By BackBtn => By.XPath(".//a[@id='btnback']");

        public By SliderMaxBtn
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public By SliderMinBtn
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }


    //Returner Loan........................................................................................................................................................................

    public class LoanPurposeDetailsMobileRLLoc : ILoanPurposeDetails
    {
        public By BtnHideShowDebug => By.XPath(".//*[@id='debug-show']");

        //Slider
        public By Slider => By.XPath(".//*[@class='ui-slider-handle ui-btn ui-shadow']");

        //public By SliderHandle => By.XPath(".//div[@class='slider-tooltip-inner']");

        //public By SliderTrack => By.XPath(".//div[@class='rc-slider-step']");

        public By SliderHandle => By.XPath(".//div[@class='display-value white text-center bold600']/span"); //new

        public By SliderTrack => By.XPath(".//div[@id='apply-slider']"); //new

        //PurposeLoanDialog
        public By PurposeLoanDialog => By.XPath("//*[@class='opensans sand-bg']");

        //AnythingElseText
        public By AnythingElseText => By.XPath("//input[@class='polMoreDetails']");

        //CancelPOL
        public By CancelPOL => By.XPath("//*[@id='cancelPolDlg']");

        //DonePurposeLoanDialog
        //public By DonePurposeLoanDialog => By.XPath(".//div[@class='polButtons dialog-buttons single-button']/input");
         public By DonePurposeLoanDialog => By.XPath(".//*[@id='purpose-done-btn2']");

        //SelectFirstPurpose
        public By SelectFirstPurpose => By.XPath("(//span[text()='- select purpose -'])[last()-2]");

        //FirstLoanAmount
        public By FirstLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[1]//div[@class='selPolSection-amountContainer']/input");

        //SelectSecondPurpose
        public By SelectSecondPurpose => By.XPath("(//span[text()='- select purpose -'])[last()-1]");

        //SecondLoanAmount
        public By SecondLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[2]//div[@class='selPolSection-amountContainer']/input");

        public By ThirdLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[3]//div[@class='selPolSection-amountContainer']/input");//new

        public By FourthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[4]//div[@class='selPolSection-amountContainer']/input");//new

        public By FifthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[5]//div[@class='selPolSection-amountContainer']/input");//new

        //BtnAddAnotherpurpose
        public By BtnAddAnotherpurpose => By.XPath(".//button[@class='isButton ui-btn ui-btn-b ui-shadow ui-corner-all']");

        //ContinueButtonRL
        public By ContinueButtonRL => By.XPath(".//*[@id='btnNext']");

        //SliderMaxBtn
        public By SliderMaxBtn => By.XPath(".//div[@class='isButton ui-btn-c maxAmount plusMinusBtn f-right font-14 charcoal']");

        //SliderMinBtn
        public By SliderMinBtn => By.XPath(".//div[@class='isButton ui-btn-c minAmount plusMinusBtn f-left font-14 charcoal']");

        //MoreInfoPOPUP
        public By MoreInfoPOPUP => By.XPath("//h2[text()='More information']");

        //MoreInfoBackBtn
        public By MoreInfoBackBtn => By.XPath("//*[@class='button-back green ui-link']");

        //MoreInformationTextArea
        public By MoreInformationTextArea => By.XPath("//*[@name='poloverallmoreinfo']");

        //MoreInformationContinue
        public By MoreInformationContinue => By.XPath(".//div[@class='popup-buttons buttons-two']/a[contains(text(),'Continue')]");

        public By ErrorMessageSamePOL => By.XPath("//div[@class='error-wrap']/p");

        public By EditMyDetailsBtn => By.XPath(".//button[@id='btnEditDetails']");

        public By PersonalDetailsHeader => By.XPath(".//div[@id='mobile-confirmdetails-details']//a[@data-page='personal']");

        public By FirstName => By.XPath(".//input[@id='firstname']");

        public By PersonalDetailsEditDoneBtn => By.XPath(".//button[@id='submit-1']");

        public By BackBtn => By.XPath(".//a[@id='btnback']");

        public By ContinueButton => By.XPath(".//*[@class='button'][contains(text(),'Continue')]");

        public By LoanWarningmsg => By.XPath(".//div[@class='error-arrow']");

        public By cancelPurposeLoanDialog => By.XPath(".//button[@id='cancelPolDlg']");
    }

    public class LoanPurposeDetailsDesktopRLLoc : ILoanPurposeDetails
    {

        public By BtnHideShowDebug => By.XPath(".//*[@id='debug-show']");
        //Slider
        public By Slider => By.XPath(".//*[@class='ui-slider-handle ui-state-default ui-corner-all']");

        //public By SliderHandle => By.XPath(".//div[@class='slider-tooltip-inner']");

        //public By SliderTrack => By.XPath(".//div[@class='rc-slider-step']");

        public By SliderHandle => By.XPath(".//div[@class='display-value white text-center bold600']/span"); //new

        public By SliderTrack => By.XPath(".//div[@id='apply-slider']"); //new

        //PurposeLoanDialog
        public By PurposeLoanDialog => By.XPath(".//div[@class='rc-dialog polDialog']");

        //AnythingElseText
        public By AnythingElseText => By.XPath("//*[@id='moredetails_1046']");

        //CancelPOL
        public By CancelPOL => By.XPath("//*[@id='cancelPolDlg']");

        //DonePurposeLoanDialog
        public By DonePurposeLoanDialog => By.XPath(".//*[@id='purpose-done-btn2']");

        //SelectFirstPurpose
        public By SelectFirstPurpose => By.XPath("(.//span[text()='- select purpose -'])[last()-2]");//new

        //FirstLoanAmount
        public By FirstLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[1]//div[@class='selPolSection-amountContainer']/input");

        //SelectSecondPurpose
        public By SelectSecondPurpose => By.XPath("(.//span[text()='- select purpose -'])[last()-1]");//new

        //SecondLoanAmount
        public By SecondLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[2]//div[@class='selPolSection-amountContainer']/input");

        //BtnAddAnotherpurpose
        public By BtnAddAnotherpurpose => By.XPath(".//button[@class='button green']");

        //ThirdLoanPurpose
        public By ThirdLoanPurpose => By.XPath("(.//span[text()='- select purpose -'])[last()]");

        //ThirdLoanAmount
        public By ThirdLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[3]//div[@class='selPolSection-amountContainer']/input");//new

        public By FourthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[4]//div[@class='selPolSection-amountContainer']/input");//new

        public By FifthLoanAmount => By.XPath(".//*[@id='selPolSections-container']/div[5]//div[@class='selPolSection-amountContainer']/input");//new

        //ContinueButtonRL
        public By ContinueButtonRL => By.XPath(".//*[@id='btnPOLsCompleted']");

        //MoreInfoPOPUP
        public By MoreInfoPOPUP => By.XPath("//span[text()='More information']");

        //MoreInfoBackBtn
        public By MoreInfoBackBtn => By.XPath("//*[@class='button-back button sml green']");

        //MoreInformationTextArea
        public By MoreInformationTextArea => By.XPath(".//textarea[@id='poloverallmoreinfo']");

        //MoreInformationContinue
        public By MoreInformationContinue => By.XPath("//*[@class='button-continue button sml button']");

        //Unused in RL
        public By LoanWarningmsg => By.XPath(".//div[@class='error-arrow']");

        public By ContinueButton => By.XPath(".//*[@class='button'][contains(text(),'Continue')]");

        public By ErrorMessageSamePOL => By.XPath("//div[@class='error-wrap']/p");

        public By EditMyDetailsBtn => By.XPath(".//a[@id='editDetailsBtn']");

        public By PersonalDetailsHeader => By.XPath(".//div[@id='detailsAccordion']/h3[@id='personalDetailsHeader']");

        public By FirstName => By.XPath(".//input[@id='firstname']");

        public By PersonalDetailsEditDoneBtn => By.XPath(".//button[@id='submit-1']");

        public By BackBtn => By.XPath(".//a[@id='btnback']");

        public By cancelPurposeLoanDialog => By.XPath(".//button[@id='cancelPolDlg']");

        public By SliderMaxBtn
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public By SliderMinBtn
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
