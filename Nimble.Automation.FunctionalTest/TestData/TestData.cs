using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble.Automation.FunctionalTest
{
    internal class TestData
    {
        public static string RunGuid { get; set; }

        public static string MACC
        {
            get
            {
                return "MACC";
            }
        }

        public static string SACC
        {
            get
            {
                return "SACC";
            }
        }

        public static string NewLoaner
        {
            get
            {
                return "New";
            }
        }

        public static string RandomPassword
        {
            get
            {
                return "password";
            }
        }

        public static string IncorrectPassword
        {
            get
            {
                return "password1234";
            }
        }

        public static string SMSCode
        {
            get
            {
                return "1234";
            }
        }

        public static string InvalidSMSCode
        {
            get
            {
                return "1122";
            }
        }

        public static string Password
        {
            get
            {
                return "password";
            }
        }

        public static string ReturnerLoaner
        {
            get
            {
                return "Returner";
            }
        }

        public static class POL
        {
            public static string Households
            {
                get
                {
                    return "Household goods and furniture";
                }
            }

            public static string RentBondRelocation
            {
                get
                {
                    return "Rent / bond / relocation";
                }
            }

            public static string CouncileorWaterRates
            {
                get
                {
                    return "Council or water rates / body corporate fees";
                }
            }

            public static string Homerepairsorimprovements
            {
                get
                {
                    return "Home repairs or improvements";
                }
            }

            public static string Partlyfullyrepayacurrentcreditcard
            {
                get
                {
                    return "Partly/fully repay a current credit card";
                }
            }

            public static string MedicalDentalexpenses
            {
                get
                {
                    return "Medical / Dental expenses";
                }
            }

            public static string Professionalservicesfees
            {
                get
                {
                    return "Professional services fees";
                }
            }

            public static string Partlyfullyrepayacurrentshorttermloan
            {
                get
                {
                    return "Partly/fully repay a current short-term loan";
                }
            }

            public static string EducationFees
            {
                get
                {
                    return "Education Fees";
                }
            }

            public static string Insurance
            {
                get
                {
                    return "Insurance";
                }
            }

            public static class TravelHoliday
            {
                public static string Airline = "Travel / holiday,Airlines and/or Accommodation";
                public static string TravelInsurance = "Travel / holiday,Travel Insurance";
            }

            public static class Eventcosts
            {
                public static string Birthdayparty = "Event costs,Birthday party";
                public static string Anniversary = "Event costs,Anniversary";
                public static string Christmasparty = "Event costs,Christmas party";
                public static string Funeralcosts = "Event costs,Funeral costs";
                public static string Weddingattire = "Event costs,Wedding attire";
                public static string Weddingreception = "Event costs,Wedding reception";
                public static string Photographyorvideographyexpenses = "Event costs,Photography or videography expenses";
                public static string Foodandbeverages = "Event costs,Food and beverages ";
            }

            public static class Onceoffproductpurchase
            {
                public static string Jewelery = "Once-off product purchase,Jewelery";
                public static string Computer = "Once-off product purchase,Computer";
                public static string Vehicle = "Once-off product purchase,Vehicle";
                public static string Boat = "Once-off product purchase,Boat";
                public static string SportingEquipment = "Once-off product purchase,Sporting Equipment";
                public static string Electronics = "Once-off product purchase,Electronics";
            }

            public static class Vehicle
            {
                public static string Registrationinsurance = "Vehicle,Registration/insurance";
                public static string Maintenance = "Vehicle,Maintenance";
            }

            public static class Other
            {
                public static string WhitegoodsEntertainment = "Other,Whitegoods/Entertainment/AV";
                public static string LandscapingRenovations = "Other,Landscaping/Renovations";
                public static string ToolsGardeningEquipment = "Other,Tools/Gardening Equipment";
                public static string RepairsandImprovements = "Other,Repairs and Improvements";
                public static string Officeequipmentsupplies = "Other,Office equipment/supplies";
                public static string VetCosts = "Other,Vet Costs";
                public static string ATO = "Other,ATO";
                public static string ProfessionalServices = "Other,Professional Services";
                public static string Fines = "Other,Fines";
                public static string Anythingelse = "Other,Anything else";
            }

            public static class Entertainmentandleisure
            {
                public static string ConcertTickets = "Entertainment and leisure,Concert Tickets";
                public static string tringThemeParkpasses = "Entertainment and leisure,Theme Park passes";
                public static string Healthandwellbeing = "Entertainment and leisure,Health and well-being";
                public static string DancesportorMusiclessons = "Entertainment and leisure,Dance";
            }

            public static class Basiclivingworkexpenses
            {
                public static string EmergencyRepairs = "Basic living/work expenses,Emergency Repairs";
                public static string Clothing = "Basic living/work expenses,Clothing";
                public static string Food = "Basic living/work expenses,Food";
                public static string Petrol = "Basic living/work expenses,Petrol";
                public static string Transport = "Basic living/work expenses,Transport";
            }

            public static class Utilitybills
            {
                public static string Onebill = "Utility bills,One bill";
                public static string Twobills = "Utility bills,Two bills";
                public static string Threebills = "Utility bills,Three bills";
                public static string Fourbillsormore = "Utility bills,Four bills or more";
            }
        }

        public static class ReasonforspeandLess
        {
            public static string cheaperproduct = "spend less,buy a cheaper product(s)";
            public static string cheaperservice = "spend less,use a cheaper service";
            public static string buyfeweritems = "spend less,buy fewer items";
        }

        public static class Debug
        {
            public static string nooverrides
            {
                get
                {
                    return "no overrides";
                }
            }

            public static string creditacceptAML1acceptBSacceptSTPaccept
            {
                get
                {
                    return "credit accept, AML 1 accept, BS accept, STP accept";
                }
            }
        }

        public static class OverrideCodes
        {
            public static string PassAll_NL
            {
                get
                {
                    //return "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:Y Rmsrv:0.9999";
                    return "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:Y Rmsv:1070 Rmsrv:0.9908";

                }
            }
            public static string PassAll_RL
            {
                get
                {
                    //return "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Rr:A Rt:8 Bsp:Y Rmsrv:0.9999";
                    return "At:N Cr:A Id:100 Rr1:A Rr2:A Rr3:A Bsp:Y Rmsv:1070 Rmsrv:0.9908";

                }
            }
            public static string Approved
            {
                get
                {
                    return " Rmsrv:0.9999";
                }
            }

            public static string Rejected
            {
                get
                {
                    return " Rmsrv:0.90000";
                }
            }
        }

        public static class YourEmployementStatus
        {
            public static string FullTime = "Full Time";
            public static string PartTime = "Part Time";
            public static string Casual = "Casual";
            public static string SelfEmployed = "Self Employed";
            public static string Contractor = "Contractor";
            public static string Unemployed = "Unemployed";
            public static string Temporary = "Temporary";
        }

        public static class ClientType
        {
            public static string SOATimeshift = " SOA Timeshift";
            public static string MACC = "MACC";
            public static string MobileApp = "MobileApp";
            public static string OldProduct = "Old Product";
            public static string NewProduct = "New Product";
            public static string Davidstestclients = "David's test clients";
            public static string BPAYAutomation = "BPAY Automation";
            public static string HighTier = "High Tier";
            public static string GracePeriod = "Grace Period";
            public static string Returners = "Returners";
            public static string NimbleCardEligibility = "Nimble Card Eligibility";
            public static string Delinquents = "Delinquents";
            public static string NimbleStatus = "Nimble Status";
            public static string CollectionMode = "Collection Mode";
        }

        public static class Feature
        {
            public static string OldSingleadvancepaidclean = "Old Product: Returner - Single advance paid clean";
            public static string OldSuspiciousInfo = "Old Product: New client - DNQ'd because 'Suspicious Info' (re-apply = auto-dnq)";
            public static string MissedRepaymentinContract = "Missed multiple repayment in contract";
            public static string MissedRepaymentinGrace = "Missed multiple repayment in grace";
            public static string ANZactivecardofferedpreEligibility = "ANZ active card offered preEligibility";
            public static string INGactivecardofferedpreEligibility = "ING active card offered preEligibility";
            public static string INGactiveweeklycardoffered = "ING active weekly card offered";
            public static string INGactivefortnightlycardoffered = "ING active fortnightly card offered";
            public static string INGactivemonthlycardoffered = "ING active monthly card offered";
            public static string ANZactiveweeklycardinEligible = "ANZ active weekly card inEligible";
            public static string ANZactivefortnightlycardinEligible = "ANZ active fortnightly card inEligible";
            public static string ANZactivemonthlycardinEligible = "ANZ active monthly card inEligible";
            public static string SACCdelinquent1 = "SACC delinquent -1";
            public static string SACCdelinquent2 = "SACC delinquent -2";
            public static string SACCdelinquent3 = "SACC delinquent -3";
            public static string MACCdelinquent1 = "MACC delinquent -1";
            public static string MACCdelinquent2 = "MACC delinquent -2";
            public static string MACCdelinquent3 = "MACC delinquent -3";
            public static string Cancelled = "Cancelled";
            public static string AutoDNQ = "Auto DNQ";
            public static string ManualDNQ = "Manual DNQ";
            public static string FinalApproval = "Final Approval";
            public static string FinalApprovalMoreInfo = "Final Approval - More Info";
            public static string NewProductAdvancePaidClean = "Returner - Advance Paid Clean - Monthly Income - May 2018";
            public static string ReturnerDagBankstaging = "Returner - DagBank - staging";
            public static string ReturnerSACCActive = "Returner - SACC - Active";
            public static string ReturnerSACCActive2prefails = "Returner - SACC - Active - 2 prefails";
            public static string ReturnerSACCActive2prefailsmissedrepayment = "Returner - SACC - Active - 2 prefails - missed - repayment";
            public static string ReturnerSACCActiveFortnightlyrepayment = "Returner - SACC - Active - fortnightly - repayment";
            public static string ReturnerSACCActiveMonthlyrepayment = "Returner - SACC - Active - monthly - repayment";
            public static string ReturnerSACCActive1prefail = "Returner - SACC - Active - 1 prefail";
            public static string ReturnerSACCActive1RemainingPayment = "Returner - SACC - Active - 1 remaining - payment";
            public static string ReturnerMACCActive = "Returner - MACC - Active";
            public static string ReturnerSACCActive2repayments = "Returner - SACC - Active - 2 repayments";

        }

        public static class YourUnEmployementDesc
        {
            public static string Student = "Student";
            public static string LookingJob = "Looking for a job";
            public static string HomeParent = "Stay at home parent";
            public static string HealthIssue = "Disability or health issue";
            public static string Retired = "Retired";
        }

        public static class NoTransactionReasons
        {
            public static string Insufficient = "Insufficient funds during period";
            public static string Usingcash = "Using cash";
            public static string Usinganother = "Using another account";
            public static string Other = "Other";
        }

        public static class BankDetails
        {
            public static class AUTOTriggerAllNoSACC
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            // return "ASICTest.bank88"; 
                            return "WebUITest.bank2";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            //return "bank88";
                            return "bank2";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            // return "ASICTest.bank88"; 
                            return "TestBank296";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            //return "bank88";
                            return "jxeLxlXE";
                        }
                    }
                }

            }

            public static class Monthly
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            // return "ASICTest.bank88"; 
                            return "WebUITest.bank56";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            //return "bank88";
                            return "bank56";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            // return "ASICTest.bank88"; 
                            return "TestBank301";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            //return "bank88";
                            return "vt#T1jw5";
                        }
                    }
                }

            }

            public static class NoTranscations
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            // return "ASICTest.bank88"; 
                            return "WebUITest.bank55";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            //return "bank88";
                            return "bank55";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            // return "ASICTest.bank88"; 
                            return "TestBank282";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            //return "bank88";
                            return "cYWw75Le";
                        }
                    }
                }

            }

            public static string Dagbank
            {
                get
                {
                    return "Dagbank";
                }
            }

            public static string ANZBank
            {
                get
                {
                    return "ANZ";
                }
            }

            public static string IncorrectPWD
            {
                get
                {
                    return "bank101";
                }
            }

            public static class IncosIncome_TC017
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank4";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank4";
                        }
                    }

                }

            }

            public static class TwoPrimaryIncome
            {
                public static class Yodlee
                {
                }
                public static string UID
                {
                    get
                    {
                        return "WebUITest.bank5";
                    }
                }

                public static string PWD
                {
                    get
                    {
                        return "bank5";
                    }
                }

            }

            public static class GamblingDNQ
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank31";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank31";
                        }
                    }
                }


            }

            public static class NoTranstFourDays
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank30";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank30";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank297";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "aO3KuF1c";
                        }
                    }
                }
            }

            public static class Five5Green1Yellow
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank33";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank33";
                        }
                    }
                }


            }

            public static class Green2Yellow3DNQ
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank32";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank32";
                        }
                    }
                }
            }

            public static class GovtInc41
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank39";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank39";
                        }
                    }
                }

            }

            public static class SingleDI
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank6";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank6";
                        }
                    }
                }


            }

            public static class GovtInc4
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank26";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank26";
                        }
                    }
                }


            }

            public static class TwoOrmoreSACCLoans
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank27";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank27";
                        }
                    }
                }


            }

            public static class AdhocTest
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank7";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank7";
                        }
                    }
                }


            }

            public static class Seven7IncomeCategories
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank8";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank8";
                        }
                    }
                }


            }

            public static class SpikeQuestion
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank9";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank9";
                        }
                    }
                }


            }

            public static class OOCQuestion
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank10";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank10";
                        }
                    }
                }


            }

            public static class HighIncome
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank12";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank12";
                        }
                    }
                }


            }

            public static class LowIncome
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank13";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank13";
                        }
                    }
                }
            }

            public static class LowIncomeLowExpenses
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank28";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank28";
                        }
                    }
                }



            }

            public static class GovtInc55
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get { return "WebUITest.bank41"; }
                    }

                    public static string PWD
                    {
                        get { return "bank41"; }
                    }
                }
            }

            public static class GovtInc81
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get { return "WebUITest.bank42"; }
                    }

                    public static string PWD
                    {
                        get { return "bank42"; }
                    }
                }
            }

            public static class GovtInc48
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get { return "WebUITest.bank43"; }
                    }

                    public static string PWD
                    {
                        get { return "bank43"; }
                    }
                }


            }

            public static class OneSAACGovtInc25
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank45";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank45";
                        }
                    }
                }
            }

            public static class Spike_OOC_Difftran
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank14";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank14";
                        }
                    }
                }


            }

            public static class Spike_OOC_SameTrans
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank15";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank15";
                        }
                    }
                }


            }

            public static class Spike_II
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank16";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank16";
                        }
                    }
                }


            }

            public static class Spike_DI
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank17";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank17";
                        }
                    }
                }


            }

            public static class OOC_II_FNT
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank18";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank18";
                        }
                    }
                }


            }

            public static class OOC_DI_MTH
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank19";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank19";
                        }
                    }
                }


            }

            public static class SpikeOOC_Same_II
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank23";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank23";
                        }
                    }
                }


            }

            public static class SpikeOOC_Dif_DI
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank25";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank25";
                        }
                    }
                }


            }

            public static class TwoBankAccounts
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank29";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank29";
                        }
                    }
                }


            }

            public static class DSC15
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank37";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank37";
                        }
                    }
                }


            }

            public static class DSC0
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank73";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank73";
                        }
                    }
                }


            }

            public static class Spike_II_MNT
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank20";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank20";
                        }
                    }
                }


            }

            public static class OneSAAC
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank44";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank44";
                        }
                    }
                }
            }

            public static class ANR_73_DI_101
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank51";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank51";
                        }
                    }
                }


            }

            public static class ANR_73_DI_97
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank52";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank52";
                        }
                    }
                }


            }

            public static class ANR_Neg21486_DI_101
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank53";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank53";
                        }
                    }
                }


            }

            public static class ANR_Neg21709_DI_97
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank54";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank54";
                        }
                    }
                }
            }

            public static class TransDesc280
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank78";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank78";
                        }
                    }
                }
            }

            public static class TransDesc2000
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank79";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank79";
                        }
                    }
                }
            }

            public static class ASICTest
            {
                public static string UID
                {
                    get
                    {
                        return "ASICTest.bank88";
                    }
                }

                public static string PWD
                {
                    get
                    {
                        return "bank88";
                    }
                }

            }


            public static class Induebank
            {
                public static string IndueLtd
                {
                    get
                    {
                        return "Indue Ltd";
                    }
                }

                public static string UID
                {
                    get
                    {
                        return "ASICTest.bank86";
                    }
                }

                public static string PWD
                {
                    get
                    {
                        return "bank86";
                    }
                }

                public static string BSB
                {
                    get
                    {
                        return "702389";
                    }
                }

                public static string AccountName
                {
                    get
                    {
                        return "TestUser";
                    }
                }

                public static string AccountNumber
                {
                    get
                    {
                        return "123456789";
                    }
                }
            }

            public static class AccountTypeBlank
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank64";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank64";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank317";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "qtpgjB5%";
                        }
                    }
                }

            }

            public static class AccountTypeCD
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank65";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank65";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank318";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "NoxnpwIH";
                        }
                    }
                }

            }

            public static class AccountTypeChecking
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank66";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank66";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank319";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "JVWhveLQ";
                        }
                    }
                }

            }

            public static class AccountTypeHomeLoan
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank67";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank67";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank320";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "mS4KJLki";
                        }
                    }
                }

            }

            public static class AccountTypeInsurance
            {
                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank321";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "APfKDGU6";
                        }
                    }
                }

            }

            public static class AccountTypeHomeInvestments
            {
                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank323";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "NuSbGxjl";
                        }
                    }
                }

            }

            public static class AccountTypeIra
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank68";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank68";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank324";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "ghTXTjP@";
                        }
                    }
                }

            }

            public static class AccountTypeNotAvailable
            {
                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank325";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "OLhlOtEU";
                        }
                    }
                }

            }

            public static class AccountTypeOther
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank69";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank69";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank326";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "04efJwSP";
                        }
                    }
                }

            }

            public static class AccountTypePersonalLoan
            {
                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank327";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "5tH3YEtE";
                        }
                    }
                }

            }

            public static class AccountTypePrepaid
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank70";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank70";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank328";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "kRhzwIUN";
                        }
                    }
                }

            }

            public static class AccountTypeSuperannuation
            {
                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank329";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "vpd1Qj%t";
                        }
                    }
                }

            }

            public static class AccountTypeTransaction
            {
                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank330";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "pe5ti@ag";
                        }
                    }
                }

            }

            public static class AccountTypeUnknown
            {
                public static class Yodlee
                {
                    public static string UID
                    {
                        get
                        {
                            return "WebUITest.bank71";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "bank71";
                        }
                    }
                }

                public static class Proviso
                {
                    public static string UID
                    {
                        get
                        {
                            return "TestBank331";
                        }
                    }

                    public static string PWD
                    {
                        get
                        {
                            return "JsOuKWDS";
                        }
                    }
                }

            }
        }

        public static class IncomeCategory
        {

            public static string PrimaryIncome
            {
                get
                {
                    return "Primary income";
                }
            }

            public static string OtherEmployment
            {
                get
                {
                    return "Other employment income";
                }
            }

            public static string SharedRentUtilities
            {
                get
                {
                    return "Shared rent/utilities";
                }
            }

            public static string InvestmentIncome
            {
                get
                {
                    return "Investment income";
                }
            }

            public static string PartnerSalary
            {
                get
                {
                    return "Partner salary";
                }
            }

            public static string NotIncome
            {
                get
                {
                    return "Not income";
                }
            }

            public static string ChildSupport
            {
                get
                {
                    return "Child support";
                }
            }
        }

        public static class ConfirmIncomeConsistency
        {

            public static string Yes
            {
                get
                {
                    return "Yes, it will stay the same (or more)";
                }
            }

            public static string No
            {
                get
                {
                    return "No, it was my old job";
                }
            }

            public static string Other
            {
                get
                {
                    return "Other (we may contact you)";
                }
            }
        }

        public static class PassportDetails
        {

            public static string IDType
            {
                get
                {
                    return "Australian Passport";
                }
            }

            public static string PassportCountry
            {
                get
                {
                    return "INDIA";
                }
            }

            public static string PassportNumber
            {
                get
                {
                    return "32569874";
                }
            }
            public static string passportbirthplace
            {
                get
                {
                    return "HYDERABAD";
                }
            }
            public static string passportbirthsurname
            {
                get
                {
                    return "RONI";
                }
            }
            public static string passportcitizenshipsurname
            {
                get
                {
                    return "RONI";
                }
            }
        }

        public static class Dependents
        {

            public static string Zero
            {
                get
                {
                    return "0";
                }
            }

            public static string One
            {
                get
                {
                    return "1";
                }
            }

            public static string Two
            {
                get
                {
                    return "2";
                }
            }

            public static string Three
            {
                get
                {
                    return "3";
                }
            }

            public static string Four
            {
                get
                {
                    return "4";
                }
            }


        }

        public class Fields
        {
            public static string EmailID(int index)
            {
                string value = string.Empty;
                switch (index)
                {
                    case 0: value = "sunil.rdl@gmail.com"; break;
                    case 1: value = "sunil.r123@gmail.com"; break;
                    case 2: value = "sunil.12@gmail.com"; break;
                    case 3: value = "sunil.5432@gmail.com"; break;
                    case 4: value = "sunil.8765@gmail.com"; break;
                    case 5: value = "sunil.876@gmail.com"; break;
                    case 6: value = "sunil.jhg@gmail.com"; break;
                    case 7: value = "sunil.jhgf@gmail.com"; break;
                }
                return value;
            }

            public static string Password { get { return ""; } }

            public static string FirstName { get { return ""; } }

            public static string MiddleName { get { return ""; } }

            public static string LastName { get { return ""; } }

            public static string DOB { get { return ""; } }

            public static string Email { get { return ""; } }

            public static string ChoosePassword { get { return ""; } }

            public static string ConfirmPassword { get { return ""; } }

            public static string HomePhone { get { return ""; } }

            public static string MobilePhone { get { return ""; } }

            public static string Address { get { return ""; } }

            public static string AddressIndex(int index)
            {
                string value = string.Empty;
                switch (index)
                {
                    case 0: value = "1"; break;
                    case 1: value = "2"; break;
                    case 2: value = "3"; break;
                    case 3: value = "4"; break;
                    case 4: value = "5"; break;
                    case 5: value = "6"; break;
                    case 6: value = "7"; break;
                    case 7: value = "8"; break;
                }
                return value;
            }

            public static class EnterPassword
            {
                public static string ChoosePassword = "Airlines and/or Accomodation";
                public static string ConfirmP = "Travel Insurance";
            }

        }

        public static string SpikeText
        {
            get
            {
                return "identified that the following transaction is higher than normal.";
            }
        }

        public static string OOCText
        {
            get
            {
                return "identified that the following transaction is out of cycle.";
            }
        }

        public static string OtherReason
        {
            get
            {
                return "That is gifted amount";
            }
        }

        public static string EmailExistingMessage
        {
            get
            {
                return "This Email is already registered with us.";
            }
        }

        public static string InvalidBSBNumber
        {
            get
            {
                return "Invalid BSB number.";
            }
        }

        public static string FraudMobileNo
        {
            get
            {
                return "0432245124";
            }
        }

    }
}

internal class GenerateRandom : IDisposable
{

    Random _random = new Random();
    int SACCMin = 300;
    int SACCMax = 1600;
    int MAACMin = 1601;
    int MACCMax = 5000;

    public string Number
    {
        get { return String(7); }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public string String(int length)
    {
        return _random.Next(1000, 999999).ToString();
    }

    public string Email()
    {
        return "AutoUser" + _random.Next(10000, 99999).ToString() + "@cigniti.com";
    }

    public int SACC()
    {
        return _random.Next(SACCMin, SACCMax);
    }

    public int MACC()
    {
        return _random.Next(MAACMin, MACCMax);
    }

}