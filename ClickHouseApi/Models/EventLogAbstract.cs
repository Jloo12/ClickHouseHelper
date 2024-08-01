using System;
using System.Text.Json;

public class EventLogAbstract
{
    public string seq_no { get; set; }               // Sequence number
    public Guid event_id { get; set; }               // Event identifier
    public string user_id { get; set; }              // User identifier
    public string event_type { get; set; }           // Type of event
    // public JsonDocument content { get; set; }        // JSON content
    public DateTime action_time { get; set; }        // Action time
    public string platform { get; set; }             // Platform
    public string os { get; set; }                   // Operating system
    public string ip_address { get; set; }           // IP address
    public string session_id { get; set; }           // Session identifier
    public string browser_id { get; set; }           // Browser identifier
    public string client_id { get; set; }            // Client identifier
    public string utm_source { get; set; }           // UTM source
    public string utm_medium { get; set; }           // UTM medium
    public string utm_campaign { get; set; }         // UTM campaign
    public uint utm_id { get; set; }                 // UTM ID
    public string utm_term { get; set; }             // UTM term
    public string utm_content { get; set; }          // UTM content
    public string page_location { get; set; }        // Page location
    public string page_referrer { get; set; }        // Page referrer
    public DateTime eng_time_msec { get; set; }      // Engagement time in milliseconds
    public string transaction_id { get; set; }       // Transaction ID
    public string currency { get; set; }             // Currency
    public uint purchase_qty { get; set; }           // Purchase quantity
    public float purchase_total { get; set; }        // Purchase total
    public string coupon { get; set; }               // Coupon
    public float coupon_disc { get; set; }           // Coupon discount
    public float order_disc { get; set; }            // Order discount
    public float price { get; set; }                 // Price
    public float value { get; set; }                 // Value
    public float tax { get; set; }                   // Tax
    public float shipping { get; set; }              // Shipping cost
    public float fee_total { get; set; }             // Total fees
    public string sign_up_method { get; set; }       // Sign-up method
    public string search_term { get; set; }          // Search term
    public string customer01 { get; set; }           // Customer attribute 01
    public string customer02 { get; set; }           // Customer attribute 02
    public string customer03 { get; set; }           // Customer attribute 03
    public string customer04 { get; set; }           // Customer attribute 04
    public string customer05 { get; set; }           // Customer attribute 05
    public string customer06 { get; set; }           // Customer attribute 06
    public string customer07 { get; set; }           // Customer attribute 07
    public string customer08 { get; set; }           // Customer attribute 08
    public string customer09 { get; set; }           // Customer attribute 09
    public string customer10 { get; set; }           // Customer attribute 10
}