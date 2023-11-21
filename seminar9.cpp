// Sa se implementeze o clasa Spital, conform urmatoarelor cerinte:
//Clasa are cel putin urmatoarele atribute:
// cif valoare de tip sir de caractere, se atribuie valoare la crearea obiectului si apoi nu mai poate fi modificata
//nume (sir de caractere) 
//adresa
//medici(vector de valori string, alocat dinamic)
//nrMedici
//nrSpitale(va retine nr spitale, obiecte de tip Spital - instantiate vreodata static int)
// metode get set
// cel putin unul dintre atribute de tip sir de caractere sa fie char*

//se vor implementa: 
// constructor fara si cu parametri, constructor de copiere
// destructor, supraincarcare operator = 
// supraincarcare operatori de citire si scriere din/in consola(>> <<)
// supraincarcare op[] - returneaza medicul de pe o pozitie data- atat in mod scriere cat si in mod citire
// supraincarcare op!
// cast explicit int, returneaza numarul de spitale instantiate
#include <iostream>
#include <string>
 
using namespace std;

class Spital
{
	const string cif;
	char* nume;
	string adresa;
	string* medici;
	int nrMedici;
	static int nrSpitale;

public:
	Spital() :cif("")
	{
		this->nume = nullptr;
		this->adresa = "";
		this->medici = nullptr;
		this->nrMedici = 0;
		Spital::nrSpitale++;
	}

	Spital(string _cif, const char* _nume, string _adresa,const string* _medici, int _nrMedici) : cif(_cif)
	{
		this->adresa = _adresa;
		if (_nume != nullptr)
		{
			this->nume = new char[strlen(_nume) + 1];
			strcpy_s(this->nume, strlen(_nume) + 1, _nume);
		}
		else
		{
			this->nume = nullptr;
		}
		if (_medici != nullptr && _nrMedici > 0)
		{
			this->nrMedici = _nrMedici;
			this->medici = new string[_nrMedici];
			for (int i = 0;i < _nrMedici;i++)
			{
				this->medici[i] = _medici[i];
			}
		}
		Spital::nrSpitale++;
	}

	Spital(const Spital& s) : cif(s.cif)
	{
		this->adresa = s.adresa;
		if (s.nume != nullptr)
		{
			this->nume = new char[strlen(s.nume) + 1];
			strcpy_s(this->nume, strlen(s.nume) + 1, s.nume);
		}
		else
		{
			this->nume = nullptr;
		}
		if (s.medici != nullptr && s.nrMedici > 0)
		{
			this->nrMedici = s.nrMedici;
			this->medici = new string[s.nrMedici];
			for (int i = 0;i < s.nrMedici;i++)
			{
				this->medici[i] = s.medici[i];
			}
		}
		else
		{
			this->nrMedici = 0;
			this->medici = nullptr;
		}

		Spital::nrSpitale--;
	}

	~Spital()
	{
		delete[] this->nume;
		delete[] this->medici;
		Spital::nrSpitale--;
	}

	Spital& operator=(const Spital& s)
	{
		if (this == &s)
		{
			return *this;
		}
		if (nume != nullptr)
		{
			delete[] this->nume;
		}
		if (medici != nullptr)
		{
			delete[] this->medici;
		}

		this->adresa = s.adresa;
		if (s.nume != nullptr)
		{
			this->nume = new char[strlen(s.nume) + 1];
			strcpy_s(this->nume, strlen(s.nume) + 1, s.nume);
		}
		else
		{
			this->nume = nullptr;
		}
		if (s.medici != nullptr && s.nrMedici > 0)
		{
			this->nrMedici = s.nrMedici;
			this->medici = new string[s.nrMedici];
			for (int i = 0;i < s.nrMedici;i++)
			{
				this->medici[i] = s.medici[i];
			}
		}
		else
		{
			this->medici = nullptr;
			this->nrMedici = 0;
		}
		return *this;
	}

	void setNume(const char* _nume)
	{
		if (_nume != nullptr)
		{
			delete[] this->nume;
			this->nume = new char[strlen(_nume) + 1];
			strcpy_s(this->nume, strlen(_nume) + 1, _nume);
		}
		else
		{
			this->nume = nullptr;
		}
	}

	void setAdresa(string _adresa){this->adresa = _adresa;}

	void setMedici(const string* _medici, int _nrMedici)
	{
		if (_medici != nullptr && _nrMedici > 0)
		{
			delete[] this->medici;
			this->nrMedici = _nrMedici;
			this->medici = new string[_nrMedici];
			for (int i = 0;i < _nrMedici;i++)
			{
				this->medici[i] = _medici[i];
			}
		}
		else
		{
			this->medici = nullptr;
			this->nrMedici = 0;
		}
	}

	void setNrMedici(int _nrMedici)
	{
		this->nrMedici = _nrMedici;
	}

	static void setNrSpitale(int _nrSpitale)
	{
		Spital::nrSpitale = _nrSpitale;
	}

	string getCif()
	{
		return cif;
	}

	char* getNume()
	{
		if (nume != nullptr)
		{
			char* copie = new char[strlen(this->nume) + 1];
			strcpy_s(copie, strlen(this->nume) + 1, this->nume);
			return copie;
		}
		else
		{
			return nullptr;
		}
	}

	int getNrMedici()
	{
		return nrMedici;
	}

	string getAdresa()
	{
		return adresa;
	}

	string* getMedici()
	{
		if (medici != nullptr)
		{
			string* copie = new string[nrMedici];
			for (int i = 0;i < nrMedici;i++)
			{
				copie[i] = medici[i];
			}
			return copie;
		}
		else
		{
			return nullptr;
		}
	}

	static int getNrSpitale()
	{
		return nrSpitale;
	}

	explicit operator int()
	{
		return Spital::nrSpitale;
	}

	bool operator!()
	{
		return nrSpitale > 0;
	}

	string& operator[](int index)
	{
		if (index >= 0 && index < nrMedici)
		{
			return medici[index];
		}
		else
		{
			cerr << "INDEX INVALID";
		}
	}
	
	Spital& operator+(int valoare)
	{
		Spital copie = *this;
		copie.nrMedici += valoare;
		return copie;
	}

	Spital& operator++(int i)
	{
		Spital copie = *this;
		copie.nrMedici += 1;
		return copie;
	}

	friend ostream& operator << (ostream& out, Spital s);
	friend istream& operator >>(istream& in, Spital& s);

};

ostream& operator << (ostream& out, Spital s)
{
	out << s.cif << endl;
	out << s.adresa << endl;
	out << s.nume << endl;
	out << s.nrMedici << endl;
	for (int i = 0;i < s.nrMedici;i++)
	{
		out << s.medici[i] << ' ';
	}
	out << "Nr spitale: " << s.getNrSpitale() << endl;

	return out;
}

istream& operator >> (istream& in, Spital& s)
{
	char* tmp = new char[256];
	cout << "Nume: ";
	in.getline(tmp, 256);
	s.setNume(tmp);
	cout << "Adresa: ";
	in.getline(tmp, 256);
	s.setAdresa(tmp);
	cout << "Nr medici: ";
	in >> s.nrMedici;
	in.ignore();
	cout << "Nume pentru " << s.nrMedici << " medici: ";
	string* medici = new string[s.nrMedici];
	for (int i = 0;i < s.nrMedici;i++)
	{
		in.getline(tmp, 256);
		medici[i] = (string)tmp;
	}
	s.setMedici(medici, s.nrMedici);
	return in;
}

int Spital::nrSpitale = 0;

int main()
{
	string* medici = new string[5];
	string* mediciS4 = new string[2];
	mediciS4[0] = "Medic 4";
	mediciS4[1] = "Medic 5";
	medici[0] = "medic1";
	medici[1] = "medic2";
	medici[2] = "medic3";
	medici[3] = "medic4";
	medici[4] = "medic5";

	Spital s1, s2("RO123", "Floreasca", "Bucuresti", medici, 5);
	Spital s4("RO456", "Sf.Pantelimon", "Bucuresti", mediciS4, 2);
	s2 = s2;
	s1 = s2;
	cout << s2 << endl;
	Spital s3;
	cin >> s3;
	cout << s1.getMedici();
	cout << (int)s3 << endl;
	cout << s3.getAdresa() << endl;
	cout << !s3 << endl;
	cout << s1[1] << endl;
	s4 = s2;
	cout << s4.getNrMedici();

	return 0;
}