using AllAPIService.IRepository;
using AllAPIService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AllAPIService.Repository
{
    public class GlobalRepository : IGlobalRepository
    {
        public TaxModel GetTaxDetails([FromBody] TaxModel data)
        {
            getTaxableIncome(data);
            GetNI(data);
            GetTax(data);
            CalculateTax(data);
            return data;
        }

        private void CalculateTax(TaxModel data)
        {
            data.NetIncome =  data.GrossSalary - Convert.ToDouble(data.Pension.Replace("%","")) - data.NiAmount - data.TaxAmount;
        }

        private void GetTax(TaxModel data)
        {
            double TAX_THRESHOLD_1 = GetTaxCodeAmount(data.PersonalAllownceCode) + 1;
            double TAX_THRESHOLD_2 = 37000;
            double TAX_THRESHOLD_3 = 50000;
            double TAX1=0;
            double TAX2=0;
            double TAX3=0;
            if (data.GrossSalary >= TAX_THRESHOLD_1)
            {
                TAX1 = data.TaxableIncome * .2;
            }
            if (data.GrossSalary - TAX_THRESHOLD_1 >= TAX_THRESHOLD_2)
            {
                TAX2 = data.TaxableIncome * .4;
            }
            if (data.GrossSalary - TAX_THRESHOLD_1 - TAX_THRESHOLD_2 >= TAX_THRESHOLD_3)
            {
                TAX3 = data.TaxableIncome * .5;
            }

            data.TaxAmount = Math.Round(TAX1 + TAX2 + TAX3, 2);
        }

        private void GetNI(TaxModel data)
        {
            data.NiAmount = 0;
            if (data.Age != "Under 66" && data.Age != null)
            {
                return;
            }

            int PRIMARY_NI_THRESHOLD_WEEKLY= 190;
            int SECONDARY_NI_THRESHOLD_WEEKLY= 175;
            double _GrossSalary = data.GrossSalary;
            if (_GrossSalary / 52 > SECONDARY_NI_THRESHOLD_WEEKLY)
            {
                data.NiAmount =
                  (_GrossSalary - SECONDARY_NI_THRESHOLD_WEEKLY * 52) *
                  0.0325;
                _GrossSalary =
                   _GrossSalary - SECONDARY_NI_THRESHOLD_WEEKLY*52;
            }

            if (_GrossSalary / 52 >= PRIMARY_NI_THRESHOLD_WEEKLY)
            {
                data.NiAmount = Math.Round(Convert.ToDouble(data.NiAmount) + _GrossSalary * 0.135342857142857, 2);
            }

        }

        private static void getTaxableIncome(TaxModel data)
        {
            double taxCode = GetTaxCodeAmount(data.PersonalAllownceCode);
            data.TaxableIncome = data.GrossSalary > taxCode ? data.GrossSalary - taxCode : data.GrossSalary;

            if (data.Pension.Contains("%"))
            {
                data.Pension = ((data.TaxableIncome * Convert.ToDouble(data.Pension.Replace("%", ""))) / 100).ToString();
                data.TaxableIncome = data.TaxableIncome - ((data.TaxableIncome * Convert.ToDouble(data.Pension.Replace("%", ""))) / 100);
            }
            else
            {
                data.TaxableIncome = data.TaxableIncome - Convert.ToDouble(data.Pension);
            }
        }

        private static double GetTaxCodeAmount(string pa)
        {
            double taxCode;
            switch (pa)
            {
                case "1257L":
                    taxCode = (double)GlobalRepositoryEnum.GlobalValues.PA1257L;
                    break;
                default:
                    taxCode = (double)GlobalRepositoryEnum.GlobalValues.PA1257L;
                    break;

            }

            return taxCode;
        }

        public List<string> GetAgeEnum()
        {
            return Enum.GetNames(typeof(GlobalRepositoryEnum.AgeEnum)).ToList();
        }
    }
}