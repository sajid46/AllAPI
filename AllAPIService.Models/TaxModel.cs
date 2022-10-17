namespace AllAPIService.Models
{
    public class TaxModel
    {
        public double GrossSalary { get; set; }
        public string PersonalAllownceCode { get; set; }
        public string Pension { get; set; }
        public double TaxableIncome { get; set; }
        public string Age { get; set; }
        public double NiAmount { get; set; }
        public double TaxAmount { get; set; }
        public double NetIncome { get; set; }
    }
}