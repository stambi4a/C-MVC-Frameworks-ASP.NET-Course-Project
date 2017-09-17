namespace ViewModels
{
    using System;

    using Helpers;

    public class PlayerViewModel
    {
        private string address;

        public int Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Address
        {
            get
            {
                return this.address;
            }

            set
            {
                this.address = value;
            }
        }

        public double PrizeMoney { get; set; }

        public int BespaPoints { get; set; }

        public PlayerImageViewModel PlayerImage { get; set; }

        public TeamShortViewModel Team { get; set; }

        public CountryShortViewModel Country { get; set; }

        public CityShortViewModel City { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Location => ViewModelsMethods.GetLocation(this.Country.Name, this.City.Name);

        public string PrizeMoneyToString => this.PrizeMoney + " лв.";

        public string DateOfBirthToString => this.DateOfBirth.ToString("dd MMMM yyyy");


        public override string ToString()
        {
            return this.Alias;
        }
    }
}
