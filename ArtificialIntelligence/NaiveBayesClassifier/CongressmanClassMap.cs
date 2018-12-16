using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace NaiveBayesClassifier
{
    public class CongressmanClassMap: ClassMap<Congressman>
    {
        public CongressmanClassMap()
        {
            AutoMap();
            Map(m => m.AdoptionOfTheBudgetResolution).TypeConverter<CongressmanConverter>();
            Map(m => m.AidToNicaraguanContras).TypeConverter<CongressmanConverter>();
            Map(m => m.AntiSatelliteTestBan).TypeConverter<CongressmanConverter>();
            Map(m => m.Crime).TypeConverter<CongressmanConverter>();
            Map(m => m.DutyFreeExports).TypeConverter<CongressmanConverter>();
            Map(m => m.EducationSpending).TypeConverter<CongressmanConverter>();
            Map(m => m.ElSalvadorAid).TypeConverter<CongressmanConverter>();
            Map(m => m.ExportAdministrationActSouthAfrica).TypeConverter<CongressmanConverter>();
            Map(m => m.HandicappedInfants).TypeConverter<CongressmanConverter>();
            Map(m => m.Immigration).TypeConverter<CongressmanConverter>();
            Map(m => m.MxMissile).TypeConverter<CongressmanConverter>();
            Map(m => m.PhysicianFeeFreeze).TypeConverter<CongressmanConverter>();
            Map(m => m.ReligiousGroupsInSchools).TypeConverter<CongressmanConverter>();
            Map(m => m.SuperfundRightToSue).TypeConverter<CongressmanConverter>();
            Map(m => m.SynfuelsCorporationCutback).TypeConverter<CongressmanConverter>();
            Map(m => m.WaterProjectCostSharing).TypeConverter<CongressmanConverter>();
        }

        class CongressmanConverter : ITypeConverter
        {
            public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
            {
                throw new System.NotImplementedException();
            }

            public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
            {
                if (text == "y")
                {
                    return Vote.Yes;
                }

                if (text == "n")
                {
                    return Vote.No;
                }

                return Vote.None;
            }
        }
    }
}
