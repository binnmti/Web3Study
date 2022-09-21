namespace NFTImageMAUIViewer;

public class IpfsJson
{
    public string title { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string imageUrl { get; set; }
    public string description { get; set; }
    public Attribute[] attributes { get; set; }
    public Properties properties { get; set; }
}

public class Properties
{
    public Name name { get; set; }
    public Description description { get; set; }
    public Preview_Media_File preview_media_file { get; set; }
    public Preview_Media_File_Type preview_media_file_type { get; set; }
    public Created_At created_at { get; set; }
    public Total_Supply total_supply { get; set; }
    public Digital_Media_Signature_Type digital_media_signature_type { get; set; }
    public Digital_Media_Signature digital_media_signature { get; set; }
    public Raw_Media_File raw_media_file { get; set; }
}

public class Name
{
    public string type { get; set; }
    public string description { get; set; }
}

public class Description
{
    public string type { get; set; }
    public string description { get; set; }
}

public class Preview_Media_File
{
    public string type { get; set; }
    public string description { get; set; }
}

public class Preview_Media_File_Type
{
    public string type { get; set; }
    public string description { get; set; }
}

public class Created_At
{
    public string type { get; set; }
    public DateTime description { get; set; }
}

public class Total_Supply
{
    public string type { get; set; }
    public int description { get; set; }
}

public class Digital_Media_Signature_Type
{
    public string type { get; set; }
    public string description { get; set; }
}

public class Digital_Media_Signature
{
    public string type { get; set; }
    public string description { get; set; }
}

public class Raw_Media_File
{
    public string type { get; set; }
    public string description { get; set; }
}

public class Attribute
{
    public string trait_type { get; set; }
    public string value { get; set; }
}

