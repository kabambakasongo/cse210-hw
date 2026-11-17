using System;

public class Address
{
    private string _street;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string street, string city, string stateProvince, string country)
    {
        _street = street;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public string Street
    {
        get { return _street; }
    }

    public string City
    {
        get { return _city; }
    }

    public string StateProvince
    {
        get { return _stateProvince; }
    }

    public string Country
    {
        get { return _country; }
    }

    public bool IsInUSA()
    {
        string country = _country.Trim();

        return country.Equals("USA", StringComparison.OrdinalIgnoreCase)
            || country.Equals("US", StringComparison.OrdinalIgnoreCase)
            || country.Equals("United States", StringComparison.OrdinalIgnoreCase);
    }

    public string GetFullAddress()
    {
        return $"{_street}\n{_city}, {_stateProvince}\n{_country}";
    }
}
