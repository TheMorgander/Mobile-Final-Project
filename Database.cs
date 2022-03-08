using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Final_Project
{
    public class Database : SQLiteOpenHelper
    {
        //Data members
        public static SQLiteDatabase database;
        private static string database_name = "History";
        private static int database_version = 1;

        //Constructor
        public Database(Context context) : base(context, database_name, null, database_version)
        {
            
        }

        //OnCreate override
        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("CREATE TABLE OPERATIONS (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "OPERATION TEXT, RESULT TEXT);");
        }

        //OnUpgrade override
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            
        }

        //Insertion
        public static void InsertOperation(SQLiteDatabase db, string operation, string result)
        {
            ContentValues operation_value = new ContentValues();
            operation_value.Put("OPERATION", operation);
            operation_value.Put("RESULT", result);
            db.Insert("OPERATIONS", null, operation_value); 
        }

        //Retreival
        public static IList<HistoryEntry> RetreiveOperations()
        {
            ICursor c = database.Query("OPERATIONS", new string[] { "_id", "OPERATION", "RESULT" }, null, null, null, null, null);

            List<HistoryEntry> history_list = new List<HistoryEntry>();

            while (c.MoveToNext())
            {
                history_list.Add(new HistoryEntry(c.GetInt(0), c.GetString(1), c.GetString(2)));
            }

            return history_list;
        }
    }

    public class HistoryEntry
    {
        public int m_id;
        public string m_operation;
        public string m_result;

        public HistoryEntry(int id, string operation, string result)
        {
            m_id = id;
            m_operation = operation;
            m_result = result;
        }
    }

    public class HistoryViewHolder : Java.Lang.Object
    {
        public TextView id { get; set; }
        public TextView operation { get; set; }
        public TextView result { get; set; }
    }

    [Activity(Label = "HistoryBaseAdapter")]
    public partial class HistoryBaseAdapter : BaseAdapter<HistoryEntry>
    {
        IList<HistoryEntry> history_list;
        private LayoutInflater mInflater;
        private Context activity;
        public HistoryBaseAdapter(Context context, IList<HistoryEntry> results)
        {
            this.activity = context;
            history_list = results;
            mInflater = (LayoutInflater)activity.GetSystemService(Context.LayoutInflaterService);
        }

        public override int Count
        {
            get { return history_list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override HistoryEntry this[int position]
        {
            get { return history_list[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            HistoryViewHolder holder = null;
            if (convertView == null)
            {
                convertView = mInflater.Inflate(Resource.Layout.history_table, null);
                holder = new HistoryViewHolder();

                holder.id = convertView.FindViewById<TextView>(Resource.Id.id_data);
                holder.operation = convertView.FindViewById<TextView>(Resource.Id.operation_data);
                holder.result = convertView.FindViewById<TextView>(Resource.Id.result_data);

                convertView.Tag = holder;
            }
            else
            {
                
                holder = convertView.Tag as HistoryViewHolder;
            }

            holder.id.Text = history_list[position].m_id.ToString();
            holder.operation.Text = history_list[position].m_operation;
            holder.result.Text = history_list[position].m_result;

            return convertView;
        }
    }
}