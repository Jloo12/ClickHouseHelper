using System;


public class ItemLogAbstract
{
    public string seq_no { get; set; }               // Sequence number
    public Guid item_log_id { get; set; }            // Item log identifier
    public Guid event_id { get; set; }               // Event identifier
    public DateTime action_time { get; set; }        // Action time
    public string transaction_id { get; set; }       // Transaction ID
    public string item_list_id { get; set; }         // Item list identifier
    public string item_list_name { get; set; }       // Item list name
    public string item_id { get; set; }              // Item identifier
    public string item_name { get; set; }            // Item name
    public string item_brand { get; set; }           // Item brand
    public string option_id { get; set; }            // Option ID
    public string option_name { get; set; }          // Option name
    public string affiliation { get; set; }          // Affiliation
    public string creative_name { get; set; }        // Creative name
    public string creative_slot { get; set; }        // Creative slot
    public string coupon { get; set; }               // Coupon
    public float coupon_disc { get; set; }           // Coupon discount
    public float order_disc { get; set; }            // Order discount
    public string item_status { get; set; }          // Item status
    public float tax { get; set; }                   // Tax
    public float price { get; set; }                 // Price
    public string currency { get; set; }             // Currency
    public float value { get; set; }                 // Value
    public string item_variant { get; set; }         // Item variant
    public string location_id { get; set; }          // Location ID
    public string promotion_id { get; set; }         // Promotion ID
    public string promotion_name { get; set; }       // Promotion name
    public string item_category { get; set; }        // Item category
    public string item_category2 { get; set; }       // Item category 2
    public string item_category3 { get; set; }       // Item category 3
    public string item_category4 { get; set; }       // Item category 4
    public string item_category5 { get; set; }       // Item category 5
    public string item_category6 { get; set; }       // Item category 6
    public string item_category7 { get; set; }       // Item category 7
    public string item_category8 { get; set; }       // Item category 8
    public string item_category9 { get; set; }       // Item category 9
    public string item_category10 { get; set; }      // Item category 10
}
