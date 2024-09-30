using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Blazor.Infrastructure.Entities.Models
{
    public class TotalByTaxScheme
    {
        public string TaxSchemeCode { get; set; }

        public string TaxSchemeName { get; set; }

        public decimal TaxPercentaje { get; set; }

        public decimal Value { get; set; }

        public decimal Quantity { get; set; }

        public decimal DiscountValue { get; set; }

        public decimal TaxValue { get; set; }


    }
    public class SectorSalud
    {
        public string CodPrestadorServicio { get;set; }

        public string TipoIdentificacionUsuario { get; set; }

        public string NumIdentificacionUsuario { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string TipoUsuario { get; set; }

        public string ModContratoPago { get; set; }

        public string Cobertura { get; set; }

        public string NumAutorizacion { get; set; }

        public string NumPrescripcion { get; set; }

        public string NumSuministroPrescripcion { get; set; }

        public string Numcontrato { get; set; }

        public string NumPoliza { get; set; }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime FechaInicio { get; set; }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime FechaFinal { get; set; }

        public Decimal Copago { get; set; }

        public Decimal CuotaModeradora { get; set; }

        public Decimal CoutaRecuperacion { get; set; }

        public Decimal PagoVoluntario { get; set; }

    }

    public static class NormalizeString
    {
        private static Regex Expression { get; set; } = new Regex("[^a-zA-Z0-9 ]");

        public static string Normalize(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
                return Expression.Replace(text.Normalize(NormalizationForm.FormD), "");
            else
                return text;
        }
    }


    [XmlRoot(ElementName = "CreditNote")]
    public class ECreditNote
    {
        [Column("IssueDate", TypeName = "datatime")]
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime IssueDate { get; set; }

        [Column("IssueDate", TypeName = "datatime")]
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "time")]
        public DateTime IssueTime { get; set; }

        public string CreditNoteTypeCode { get; set; }

        public string CustomizationID { get; set; }

        public string ID { get; set; }

        public string DocumentCurrencyCode { get; set; }

        public InvoicePeriod CreditNotePeriod { get; set; }

        public DiscrepancyResponse DiscrepancyResponse { get; set; }

        public List<InvoiceDocumentReference> BillingReference { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public SellerSupplierParty SellerSupplierParty { get; set; }

        public AccountingCustomerParty AccountingCustomerParty { get; set; }

        public PaymentMeans PaymentMeans { get; set; }

        [XmlElementAttribute("TaxTotal")]
        public List<TaxTotal> TaxTotal { get; set; }

        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }

        [XmlElementAttribute("CreditNoteLine")]
        public List<CreditLine> CreditNoteLine { get; set; }

        public string SerializeToXml()
        {
            UTF8Encoding encoding = new UTF8Encoding(false);

            var xmlAttributes = new XmlAttributes();
            xmlAttributes.Xmlns = false;
            xmlAttributes.XmlType = new XmlTypeAttribute() { Namespace = "" };


            XmlSerializer sr = new XmlSerializer(this.GetType());

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream, encoding);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            // get the stream from the writer
            memoryStream = writer.BaseStream as MemoryStream;

            sr.Serialize(writer, this, namespaces);

            // apply encoding to the stream 
            string xml = (encoding.GetString(memoryStream.ToArray()).Trim()).Replace(@" d4p1:nil=""true"" xmlns:d4p1=""http://www.w3.org/2001/XMLSchema-instance"" ", "").Replace(@" d5p1:nil=""true"" xmlns:d5p1=""http://www.w3.org/2001/XMLSchema-instance"" ", "");

            return xml;
        }

    }

    [XmlRoot(ElementName = "DebitNote")]
    public class EDebitNote
    {
        [Column("IssueDate", TypeName = "datatime")]
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime IssueDate { get; set; }

        [Column("IssueDate", TypeName = "datatime")]
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "time")]
        public DateTime IssueTime { get; set; }

        public string DebitNoteTypeCode { get; set; }

        public string CustomizationID { get; set; }

        public string ID { get; set; }

        public string DocumentCurrencyCode { get; set; }

        public InvoicePeriod DebitNotePeriod { get; set; }

        public DiscrepancyResponse DiscrepancyResponse { get; set; }

        public List<InvoiceDocumentReference> BillingReference { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public SellerSupplierParty SellerSupplierParty { get; set; }

        public AccountingCustomerParty AccountingCustomerParty { get; set; }

        public PaymentMeans PaymentMeans { get; set; }

        [XmlElementAttribute("TaxTotal")]
        public List<TaxTotal> TaxTotal { get; set; }

        public LegalMonetaryTotal RequestedMonetaryTotal { get; set; }

        [XmlElementAttribute("DebitNoteLine")]
        public List<DebitLine> DebitNoteLine { get; set; }

        public string SerializeToXml()
        {
            UTF8Encoding encoding = new UTF8Encoding(false);

            var xmlAttributes = new XmlAttributes();
            xmlAttributes.Xmlns = false;
            xmlAttributes.XmlType = new XmlTypeAttribute() { Namespace = "" };

            XmlSerializer sr = new XmlSerializer(this.GetType());

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream, encoding);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            // get the stream from the writer
            memoryStream = writer.BaseStream as MemoryStream;

            sr.Serialize(writer, this, namespaces);

            // apply encoding to the stream 
            string xml = (encoding.GetString(memoryStream.ToArray()).Trim()).Replace(@" d4p1:nil=""true"" xmlns:d4p1=""http://www.w3.org/2001/XMLSchema-instance"" ", "").Replace(@" d5p1:nil=""true"" xmlns:d5p1=""http://www.w3.org/2001/XMLSchema-instance"" ", "");

            return xml;
        }
    }


    //[XmlRoot(ElementName = "InvoiceDocumentReference")]
    public class InvoiceDocumentReference
    {
        public string ID { get; set; }

        public string UUID { get; set; }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime IssueDate { get; set; }
        
    }

    public class DiscrepancyResponse
    {
        public string ReferenceID { get; set; }

        public string ResponseCode { get; set; }

        public string Description { get; set; }
    }


    [XmlRoot(ElementName = "Invoice")]
    public class EInvoice
    {

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime IssueDate { get; set; }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "time")]
        public DateTime IssueTime { get; set; }

        public string InvoiceTypeCode { get; set; }

        public string CustomizationID { get; set; }

        public string ID { get; set; }

         public string DocumentCurrencyCode { get; set; }

        public InvoicePeriod InvoicePeriod { get; set; }

        [XmlElementAttribute("SectorSalud")]
        public List<SectorSalud> SectorSalud { get; set; }

        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        public SellerSupplierParty SellerSupplierParty { get; set; }

        public AccountingCustomerParty AccountingCustomerParty { get; set; }

        public PaymentMeans PaymentMeans { get; set; }

        [XmlElementAttribute("PrePaidPayment")]
        public List<PrePaidPayment> PrePaidPayment { get; set; }

        [XmlElementAttribute("TaxTotal")]
        public List<TaxTotal> TaxTotal { get; set; }

        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }

        [XmlElementAttribute("InvoiceLine")]
        public List<InvoiceLine> InvoiceLine { get; set; }

        public string Notes1 { get; set; }

        public string Notes2 { get; set; }

        public string Notes3 { get; set; }

        public string Notes4 { get; set; }

        public string Notes5 { get; set; }

        public string MailReceptor { get; set; }

        public InvoiceControl InvoiceControl { get; set; }

        public string SerializeToXml()
        {
            UTF8Encoding encoding = new UTF8Encoding(false);

            var xmlAttributes = new XmlAttributes();
            xmlAttributes.Xmlns = false;
            xmlAttributes.XmlType = new XmlTypeAttribute() { Namespace = "" };

            XmlSerializer sr = new XmlSerializer(this.GetType());

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream, encoding);

            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            // get the stream from the writer
            memoryStream = writer.BaseStream as MemoryStream;

            sr.Serialize(writer, this, namespaces);

            // apply encoding to the stream 
            string xml = (encoding.GetString(memoryStream.ToArray()).Trim()).Replace(@" d4p1:nil=""true"" xmlns:d4p1=""http://www.w3.org/2001/XMLSchema-instance"" ", "").Replace(@" d5p1:nil=""true"" xmlns:d5p1=""http://www.w3.org/2001/XMLSchema-instance"" ", "");

            return xml;
        }
    }

    public class AllowanceCharge
    {
        public int ID { get; set; }

        public bool ChargeIndicator { get; set; }

        public string AllowanceChargeReason { get; set; }

        public decimal MultiplierFactorNumeric { get; set; }

        public decimal BaseAmount { get; set; }

        public decimal Amount { get; set; }

    }


    public class PrePaidPayment
    {
        public int ID { get; set; }

        public decimal PaidAmount { get; set; }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime ReceivedDate { get; set; }
    }


    public class InvoiceControl
    {
        public long InvoiceAuthorization { get; set; }

        public InvoicePeriod AuthorizationPeriod { get; set; }

        public AuthorizedInvoice AuthorizedInvoices { get; set; }

    }

    public class AuthorizedInvoice
    {
        public string Prefix { get; set; }

        public long From { get; set; }

        public long To { get; set; }

        public string TechnicalKey { get; set; }

    }

    public class DecimalType
    {
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value { get; set; }
    }

    public class IntType
    {
        public int Value { get; set; }
    }

    public class StringType
    {
        public string Value { get; set; }
    }

    public class DateType
    {
        [System.Xml.Serialization.XmlTextAttribute(DataType = "date")]
        public DateTime Value { get; set; }
    }


    public class TimeType
    {
        [System.Xml.Serialization.XmlTextAttribute(DataType = "time")]
        public DateTime Value { get; set; }
    }

    public class InvoiceLine
    {
        public int ID { get; set; }

        public string Note { get; set; }

        public StandardItemIdentification StandardItemIdentification { get; set; }

        public SellersItemIdentification SellersItemIdentification { get; set; }

        public decimal InvoicedQuantity { get; set; }

        public string UnitCode { get; set; }

        public PackQuantityType PackQuantity { get; set; }

        public LotNumberType LotNumber { get; set; }

        public LotExpiredDateType LotExpiredDate { get; set; }


        [XmlElement("LineExtensionAmount")]
        public string LineExtensionAmountString
        {
            get
            {
                return String.Format("{0:0.00}", LineExtensionAmount).Replace(",", ".");
            }
            set
            {
                decimal amount = 0;
                if (Decimal.TryParse(value, out amount))
                    LineExtensionAmount = amount;

            }
        }

        [XmlIgnore]
        public decimal LineExtensionAmount { get; set; }

        public NetLineExtensionAmountType NetLineExtensionAmount { get; set; }

        public Price Price { get; set; } = new Price();

        public PricingReference PricingReference { get; set; }

        public Item Item { get; set; }

        [XmlElementAttribute("AllowanceCharge")]
        public List<AllowanceCharge> AllowanceCharge { get; set; }

        public TaxTotal TaxTotal { get; set; }

    }

    public class CreditLine
    {
        public int ID { get; set; }

        public string Note { get; set; }

        public StandardItemIdentification StandardItemIdentification { get; set; }

        public SellersItemIdentification SellersItemIdentification { get; set; }

        public decimal CreditedQuantity { get; set; }

        public string UnitCode { get; set; }

        public PackQuantityType PackQuantity { get; set; }

        public LotNumberType LotNumber { get; set; }

        public LotExpiredDateType LotExpiredDate { get; set; }

        [XmlElement("LineExtensionAmount")]
        public string LineExtensionAmountString
        {
            get
            {
                return String.Format("{0:0.00}", LineExtensionAmount).Replace(",", ".");
            }
            set
            {
                decimal amount = 0;
                if (Decimal.TryParse(value, out amount))
                    LineExtensionAmount = amount;

            }
        }

        [XmlIgnore]
        public decimal LineExtensionAmount { get; set; }

        public NetLineExtensionAmountType NetLineExtensionAmount { get; set; }

        public Price Price { get; set; } = new Price();

        public PricingReference PricingReference { get; set; }

       
        public Item Item { get; set; }

        [XmlElementAttribute("AllowanceCharge")]
        public List<AllowanceCharge> AllowanceCharge { get; set; }


        public TaxTotal TaxTotal { get; set; }

    }

    public class DebitLine
    {
        public int ID { get; set; }

        public string Note { get; set; }

        public StandardItemIdentification StandardItemIdentification { get; set; }

        public SellersItemIdentification SellersItemIdentification { get; set; }

        public decimal DebitedQuantity { get; set; }

        public string UnitCode { get; set; }

        public PackQuantityType PackQuantity { get; set; }

        public LotNumberType LotNumber { get; set; }

        public LotExpiredDateType LotExpiredDate { get; set; }

        [XmlElement("LineExtensionAmount")]
        public string LineExtensionAmountString
        {
            get
            {
                return String.Format("{0:0.00}", LineExtensionAmount).Replace(",", ".");
            }
            set
            {
                decimal amount = 0;
                if (Decimal.TryParse(value, out amount))
                    LineExtensionAmount = amount;

            }
        }

        [XmlIgnore]
        public decimal LineExtensionAmount { get; set; }

        public NetLineExtensionAmountType NetLineExtensionAmount { get; set; }

        public Price Price { get; set; } = new Price();

        public PricingReference PricingReference { get; set; }

        public Item Item { get; set; }

        [XmlElementAttribute("AllowanceCharge")]
        public List<AllowanceCharge> AllowanceCharge { get; set; }

        public TaxTotal TaxTotal { get; set; }

    }

    public class Item
    {
        public void AddDescription(string desc)
        {
            if (Description == null)
                Description = new List<string>();
            Description.Add(Regex.Replace(desc, @"[^a-zA-z0-9 ]+", ""));
        }

        [XmlElementAttribute("Description")]
        public List<string> Description { get; set; }

        public PackSizeNumericType PackSizeNumeric { get; set; }

        public string BrandName { get; set; }

        public string ModelName { get; set; }

        public List<AdditionalItemProperty> AdditionalItemProperty { get; set; }

        
    }

    public class AdditionalItemProperty
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public class PricingReference
    {
        public decimal PriceAmount { get; set; }

        public string PriceTypeCode { get; set; }
    }

    public class Price
    {
        public decimal PriceAmount { get; set; }

        public decimal BaseQuantity { get; set; }

        public NetPriceAmountType NetPriceAmount { get; set; }
    }

    public class SellersItemIdentification
    {
        public string ID { get; set; }

        public string ExtendedID { get; set; }
    }

    public class StandardItemIdentification
    {
        public string ID { get; set; }

        public string schemeID { get; set; }

    }

    [XmlRoot(ElementName = "AllowanceTotalAmount")]
    public class AllowanceTotalAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "AllowanceLineAmount")]
    public class AllowanceLineAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "ChargeTotalAmount")]
    public class ChargeTotalAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "PrePaidAmount")]
    public class PrePaidAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "TaxBaseExemptAmount")]
    public class TaxBaseExemptAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "TaxBaseExcludedAmount")]
    public class TaxBaseExcludedAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "RoundingAmount")]
    public class RoundingAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "QuantityCountNumeric")]
    public class QuantityCountNumericType : DecimalType
    {
    }

    [XmlRoot(ElementName = "PackQuantityNumeric")]
    public class PackQuantityNumericType : DecimalType
    {
    }

    [XmlRoot(ElementName = "PackQuantity")]
    public class PackQuantityType : DecimalType
    {
    }

    [XmlRoot(ElementName = "NetPriceAmount")]
    public class NetPriceAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "PackSizeNumeric")]
    public class PackSizeNumericType : DecimalType
    {
    }

    [XmlRoot(ElementName = "NetLineExtensionAmount")]
    public class NetLineExtensionAmountType : DecimalType
    {
    }

    [XmlRoot(ElementName = "LotNumber")]
    public class LotNumberType : IntType
    {
    }

    [XmlRoot(ElementName = "LineCountNumeric")]
    public class LineCountNumericType : DecimalType
    {
    }

    [XmlRoot(ElementName = "LotExpiredDate")]
    public class LotExpiredDateType : DateType
    {
    }

    public class LegalMonetaryTotal
    {
        public decimal LineExtensionAmount { get; set; }

        public decimal TaxExclusiveAmount { get; set; }

        public decimal TaxExclusiveBaseAmount { get; set; }

        public decimal TaxInclusiveAmount { get; set; }

        public AllowanceTotalAmountType AllowanceTotalAmount { get; set; }

        public AllowanceLineAmountType AllowanceLineAmount { get; set; }

        public ChargeTotalAmountType ChargeTotalAmount { get; set; }

        public PrePaidAmountType PrePaidAmount { get; set; }

        public decimal PayableAmount { get; set; }

        public decimal PayableExpectedAmount { get; set; }

        public TaxBaseExemptAmountType TaxBaseExemptAmount { get; set; }

        public TaxBaseExcludedAmountType TaxBaseExcludedAmount { get; set; }

        public RoundingAmountType RoundingAmount { get; set; }

        public string TextAmount { get; set; }

        public QuantityCountNumericType QuantityCountNumeric { get; set; }

        public PackQuantityNumericType PackQuantityNumeric { get; set; }

        public LineCountNumericType LineCountNumeric { get; set; }

    }


    public class TaxTotal
    {

        [XmlElement("TaxAmount")]
        public string TaxAmountString
        {
            get
            {
                return String.Format("{0:0.00}", TaxAmount).Replace(",","."); 
            }
            set
            {
                decimal amount = 0;
                if (Decimal.TryParse(value, out amount))
                    TaxAmount = amount;

            }
        }

        [XmlIgnore]
        public Decimal TaxAmount { get; set; }

        public TaxSubtotal TaxSubtotal { get; set; }


    }

    [XmlRoot(ElementName = "PerUnitAmount")]
    public class PerUnitAmountType : DecimalType
    {
    }

    public class TaxSubtotal
    {

        [XmlElement("TaxableAmount")]
        public string TaxableAmountString
        {
            get
            {
                return String.Format("{0:0.00}", TaxableAmount).Replace(",", ".");
            }
            set
            {
                decimal amount = 0;
                if (Decimal.TryParse(value, out amount))
                    TaxableAmount = amount;

            }
        }

        [XmlIgnore]
        public decimal TaxableAmount { get; set; }

        [XmlElement("TaxAmount")]
        public string TaxAmountString
        {
            get
            {
                return String.Format("{0:0.00}", TaxAmount).Replace(",", ".");
            }
            set
            {
                decimal amount = 0;
                if (Decimal.TryParse(value, out amount))
                    TaxAmount = amount;

            }
        }

        [XmlIgnore]
        public decimal TaxAmount { get; set; }

        public string BaseUnitMeasure { get; set; }

        public string UnitCode { get; set; }

        public PerUnitAmountType PerUnitAmount { get; set; }

        public TaxCategory TaxCategory { get; set; }

    }

    [XmlRoot(ElementName = "Percent")]
    public class PercentType : DecimalType
    {
    }

    public class TaxCategory
    {
        public PercentType Percent { get; set; }

        public TaxScheme TaxScheme { get; set; }
    }

    public class PaymentMeans
    {
        public string ID { get; set; }

        public string PaymentMeansCode { get; set; }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime PaymentDueDate { get; set; }

        public string PaymentID { get; set; }

        public string Note { get; set; }
    }

    public class PartyTaxScheme2
    {

        private string registrationName;

        public string RegistrationName
        {
            get
            {
                return NormalizeString.Normalize(registrationName);
            }
            set
            {
                registrationName = value;
            }
        }

        public string TaxLevelCode { get; set; }

        public string ListName { get; set; }

        public TaxScheme TaxScheme { get; set; }

        public string CorporateRegistrationScheme { get; set; }

        public Address RegistrationAddress { get; set; }

    }

    public class Party2
    {
        public string PartyIdentification { get; set; }

        public string ID { get; set; }

        public string schemeID { get; set; }

        public PartyName PartyName { get; set; }

        public PhysicalLocation PhysicalLocation { get; set; }

        public PartyTaxScheme2 PartyTaxScheme { get; set; }

        public Person Person { get; set; }

        public Contact Contact { get; set; }

        public ClientCode ClientCode { get; set; }
    }

    public class ClientCode
    {
        public string ID { get; set; }
    }

    public class Person
    {
        private string firstName;
        public string FirstName
        {
            get { return NormalizeString.Normalize(firstName); }
            set { firstName = value; }
        }

        private string lastName;
        public string LastName
        {
            get { return NormalizeString.Normalize(lastName); }
            set { lastName = value; }
        }

        private string middleName;
        public string MiddleName
        {
            get { return NormalizeString.Normalize(middleName); }
            set { middleName = value; }
        }

    }

    public class Contact
    {
        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Mobilephone { get; set; }

        public string ElectronicMail { get; set; }

    }

    public class AccountingCustomerParty
    {
        public string AdditionalAccountID { get; set; }

        public Party2 Party { get; set; }

        public PartyName PartyName { get; set; }

        public PhysicalLocation PhysicalLocation { get; set; }

        public PartyTaxScheme2 PartyTaxScheme { get; set; }
    }

    public class SellerSupplierParty
    {
        public string ID { get; set; }

        public string CorporateRegistrationScheme { get; set; }

        public Party1 Party { get; set; }

    }

    [ComplexType]
    public class AccountingSupplierParty
    {
        public string AdditionalAccountID { get; set; }

        public Party Party { get; set; }
    }

    public class Party1
    {
        public PartyName PartyName { get; set; }

        public PhysicalLocation PhysicalLocation { get; set; }
    }

    [ComplexType]
    public class Party
    {
        public string IndustryClassificationCode { get; set; }

        public string PartyIdentification { get; set; }

        public string ID { get; set; }

        public string schemeID { get; set; }

        public PartyName PartyName { get; set; }

        public PhysicalLocation PhysicalLocation { get; set; }

        public string WebSite { get; set; }

        public string PBX { get; set; }

        public PartyTaxScheme PartyTaxScheme { get; set; }

        public SellerContact SellerContact { get; set; }


    }

    public class SellerContact
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string ElectronicMail { get; set; }

        public AddressLine AddressLine { get; set; }
    }

    public class PartyTaxScheme
    {
        private string registrationName;
        public string RegistrationName
        {
            get { return NormalizeString.Normalize(registrationName); }
            set { registrationName = value; }
        }

        public string TaxLevelCode { get; set; }

        public string ListName { get; set; }

        public TaxScheme TaxScheme { get; set; }

    }

    public class TaxScheme
    {
        public string ID { get; set; }

        public string Name { get; set; }
    }

    [ComplexType]
    public class PhysicalLocation
    {
        public Address Address { get; set; }
    }

    [ComplexType]
    public class Address
    {
        public string ID { get; set; }

        private string cityName;

        public string CityName
        {
            get { return string.IsNullOrWhiteSpace(cityName) ? cityName : Regex.Replace(cityName, @"[^a-zA-z0-9 ]+", ""); }
            set { cityName = value; }
        }

        public string PostalZone { get; set; }

        private string countrySubentity;

        public string CountrySubentity
        {
            get { return string.IsNullOrWhiteSpace(countrySubentity) ? countrySubentity : Regex.Replace(countrySubentity, @"[^a-zA-z0-9 ]+", ""); }
            set { countrySubentity = value; }
        }

        public string CountrySubentityCode { get; set; }

        public AddressLine AddressLine { get; set; }

        public CountryE Country { get; set; }

    }

    public class CountryE
    {
        public string IdentificationCode { get; set; }

        private string name;
        public string Name
        {
            get { return string.IsNullOrWhiteSpace(name) ? name : Regex.Replace(name, @"[^a-zA-z0-9 ]+", ""); }
            set { name = value; }
        }

    }

    [ComplexType]
    public class AddressLine
    {
        private string line;

        public string Line
        {
            get { return string.IsNullOrWhiteSpace(line) ? line : Regex.Replace(line, @"[^a-zA-z0-9 ]+", ""); }
            set { line = value; }
        }

    }

    [ComplexType]
    public class PartyName
    {
        private string name;
        public string Name
        {
            get { return NormalizeString.Normalize(name); }
            set { name = value; }
        }

    }

    [XmlRoot(ElementName = "StartTime")]
    public class StartTimeType : TimeType
    {
    }

    [XmlRoot(ElementName = "EndTime")]
    public class EndTimeType : TimeType
    {
    }

    [ComplexType]
    public class InvoicePeriod
    {
        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime StartDate { get; set; }

        [XmlElementAttribute("StartTime")]
        public StartTimeType StartTime { get; set; }

        [XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "date")]
        public DateTime EndDate { get; set; }

        [XmlElementAttribute("EndTime")]
        public EndTimeType EndTime { get; set; }
    }

}