sqlcmd -S .\SQL2012 -d Preclone_Client -E -s"	" -W -Q "SELECT distinct objrel.TechnicalObjectKey, [lookup].ObjectSystem.Description, 'BELONGS_TO_SYSTEM' as REl FROM ObjectRelation objrel inner join [lookup].ObjectSystem on objrel.ObjectSystemId = [lookup].ObjectSystem.ObjectSystemId" > C:\Users\Steven.Suing\Desktop\batch_importer_20\ObjecttoSystemRel.csv