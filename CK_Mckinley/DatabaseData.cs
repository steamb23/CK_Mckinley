using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace CK_Mckinley
{
    public enum DBMajor
    {
        ETC = 1,
        Programming,
        Graphic,
        Design
    }
    public enum DBGenre
    {
        ETC = 1,
        RPG,
        FPS,
        TPS,
        Adventure,
        Action,
        Puzzle,
        Racing,
        Fight,
        Arcade,
        Simulation,
        SNG,
        Rhythm,
        Serious,
        RTS,
        Sports,
        Casual,
        Board
    }
    public enum DBDimension
    {
        ETC = 1,
        _2D,
        _3D,
    }
    public enum DBPlatform
    {
        ETC = 1,
        PC,
        Mobile,
        Web,
        XBOX,
        PS,
        Nintendo
    }
    public enum DBYear
    {
        ETC = 1,
        _1999,
        _2000,
        _2001,
        _2002,
        _2003,
        _2004,
        _2005,
        _2006,
        _2007,
        _2008,
        _2009,
        _2010,
        _2011,
        _2012,
        _2013,
        _2014,
        _2015
    }
    public enum DBWorld
    {
        ETC = 1,
        Fantasy,
        SF,
        Oriental,
        History,
        Space,
        Horror,
        Modern,
        Fairytale
    }
    public enum DBEngine
    {
        ETC = 1,
        DirectX,
        Unity,
        Unreal,
        Gamebryo,
        Cocos2Dx,
    }
    public enum DBResourceType
    {
        ETC = 1,
        Design,
        Design_By_Products,
        Program_Source,
        Program_Design,
        Art,
        Illustration,
        Poster,
        Icon,
        Logo,
        UI,
        _2D_Animation,
        _2D_Sprite,
        _2D_Effect,
        _3D_Model,
        _3D_Effect,
        Texture,
        _3D_Animation,
        Sound_Effect,
        Sound_Background,
        Sound_Voice,
        Movie
    }
    public class DatabaseData
    {
        MySqlParameter id = new MySqlParameter("@id", MySqlDbType.Int32);
        MySqlParameter team = new MySqlParameter("@team", MySqlDbType.Text);
        MySqlParameter owner = new MySqlParameter("@owner", MySqlDbType.Text);
        MySqlParameter major = new MySqlParameter("@major", MySqlDbType.Enum);

        MySqlParameter genre = new MySqlParameter("@genre", MySqlDbType.Enum);
        MySqlParameter dimension = new MySqlParameter("@dimension", MySqlDbType.Enum);
        MySqlParameter platform = new MySqlParameter("@platform", MySqlDbType.Enum);
        MySqlParameter year = new MySqlParameter("@year", MySqlDbType.Enum);
        MySqlParameter world = new MySqlParameter("@world", MySqlDbType.Enum);
        MySqlParameter engine = new MySqlParameter("@engine", MySqlDbType.Enum);
        MySqlParameter resourceType = new MySqlParameter("@resource_type", MySqlDbType.Enum);

        MySqlParameter etcTag = new MySqlParameter("@etc", MySqlDbType.Text);
        MySqlParameter fileName = new MySqlParameter("@file", MySqlDbType.Text);
        MySqlParameter thumnailName = new MySqlParameter("@thumnail", MySqlDbType.Text);

        public DatabaseData()
        {
        }
        public DatabaseData(MySqlDataReader reader)
        {
            this.SetData(reader);
        }

        public long Id
        {
            get
            {
                return (long)id.Value;
            }
            set
            {
                id.Value = value;
            }
        }
        public string Team
        {
            get
            {
                return (string)team.Value;
            }
            set
            {
                team.Value = value;
            }
        }
        public string Owner
        {
            get
            {
                return (string)owner.Value;
            }
            set
            {
                owner.Value = value;
            }
        }
        public DBMajor Major
        {
            get
            {
                if (major.Value != null)
                    return (DBMajor)major.Value;
                else
                    return (DBMajor)1;
            }
            set
            {
                major.Value = value;
            }
        }

        public DBGenre Genre
        {
            get
            {
                if (genre.Value != null)
                    return (DBGenre)genre.Value;
                else
                    return (DBGenre)1;
            }
            set
            {
                genre.Value = value;
            }
        }
        public DBDimension Dimension
        {
            get
            {
                if (dimension.Value != null)
                    return (DBDimension)dimension.Value;
                else
                    return (DBDimension)1;
            }
            set
            {
                dimension.Value = value;
            }
        }

        public DBPlatform Platform
        {
            get
            {
                if (platform.Value != null)
                    return (DBPlatform)platform.Value;
                else
                    return (DBPlatform)1;
            }
            set
            {
                platform.Value = value;
            }
        }
        public DBYear Year
        {
            get
            {
                if (year.Value != null)
                    return (DBYear)year.Value;
                else
                    return (DBYear)1;
            }
            set
            {
                year.Value = value;
            }
        }
        public DBWorld World
        {
            get
            {
                if (world.Value != null)
                    return (DBWorld)world.Value;
                else
                    return (DBWorld)1;
            }
            set
            {
                world.Value = value;
            }
        }
        public DBEngine Engine
        {
            get
            {
                if (engine.Value != null)
                    return (DBEngine)engine.Value;
                else
                    return (DBEngine)1;
            }
            set
            {
                engine.Value = value;
            }
        }
        public DBResourceType ResourceType
        {
            get
            {
                if (resourceType.Value != null)
                    return (DBResourceType)resourceType.Value;
                else
                    return (DBResourceType)1;
            }
            set
            {
                resourceType.Value = value;
            }
        }

        public string EtcTag
        {
            get
            {
                return (string)etcTag.Value;
            }
            set
            {
                etcTag.Value = value;
            }
        }
        public string FileName
        {
            get
            {
                return (string)fileName.Value;
            }
            set
            {
                fileName.Value = value;
            }
        }
        public string ThumnailName
        {
            get
            {
                return (string)thumnailName.Value;
            }
            set
            {
                thumnailName.Value = value;
            }
        }

        public void SetData(MySqlDataReader reader)
        {
            this.Id = reader.GetInt64("id");
            this.Team = reader.GetString("team");
            this.Owner = reader.GetString("owner");
            this.Major = StringToEnum<DBMajor>(reader.GetString("major"));

            this.Genre = StringToEnum<DBGenre>(reader.GetString("genre"));
            this.Dimension = StringToEnum<DBDimension>(reader.GetString("dimension"));
            this.Platform = StringToEnum<DBPlatform>(reader.GetString("platform"));
            this.Year = StringToEnum<DBYear>(reader.GetString("year"));
            this.World = StringToEnum<DBWorld>(reader.GetString("world"));
            this.Engine = StringToEnum<DBEngine>(reader.GetString("engine"));
            this.ResourceType = StringToEnum<DBResourceType>(reader.GetString("resource_type"));
            this.EtcTag = reader.GetString("etc");

            this.FileName = reader.GetString("file");
            this.ThumnailName = reader.GetString("thumnail");
        }

        public void Insert(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(@"
insert
into `ckgame`.`ckgame_mckinley`(`team`, `owner`, `major`, `genre`, `dimension`, `platform`, `year`, `world`, `engine`, `resource_type`, `etc`, `file`, `thumnail`)
values (@team, @owner, @major, @genre, @dimension, @platform, @year, @world, @engine, @resource_type, @etc, @file, @thumnail);", connection);

            command.Parameters.Add(team);
            command.Parameters.Add(owner);
            command.Parameters.Add(major);
            command.Parameters.Add(genre);
            command.Parameters.Add(dimension);
            command.Parameters.Add(platform);
            command.Parameters.Add(year);
            command.Parameters.Add(world);
            command.Parameters.Add(engine);
            command.Parameters.Add(resourceType);
            command.Parameters.Add(etcTag);
            command.Parameters.Add(fileName);
            command.Parameters.Add(thumnailName);

            command.ExecuteNonQuery();

            this.Id = command.LastInsertedId;
        }
        public void Update(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(@"
update `ckgame`.`ckgame_mckinley`
set
`team` = @team,
`owner` = @owner,
`major` = @major,
`genre` = @genre,
`dimension` = @dimension,
`platform` = @platform,
`year` = @year,
`world` = @world,
`engine` = @engine,
`resource_type` = @resource_type,
`etc` = @etc,
`file` = @file,
`thumnail` = @thumnail
where id = @id;", connection);
            command.Parameters.Add(id);

            command.Parameters.Add(team);
            command.Parameters.Add(owner);
            command.Parameters.Add(major);
            command.Parameters.Add(genre);
            command.Parameters.Add(dimension);
            command.Parameters.Add(platform);
            command.Parameters.Add(year);
            command.Parameters.Add(world);
            command.Parameters.Add(engine);
            command.Parameters.Add(resourceType);
            command.Parameters.Add(etcTag);
            command.Parameters.Add(fileName);
            command.Parameters.Add(thumnailName);

            command.ExecuteNonQuery();
        }

        public void Delete(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(@"
delete from `ckgame`.`ckgame_mckinley`
where id = @id;", connection);

            command.Parameters.Add(id);

            command.ExecuteNonQuery();
        }

        public DataTable AllData(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(@"
select * from `ckgame`.`ckgame_mckinley`", connection);

            using (var reader = command.ExecuteReader())
            {
                return reader.GetSchemaTable();
            }
        }
        public void SelectId(MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(@"
select * from `ckgame`.`ckgame_mckinley`
where `id` = @id", connection);

            command.Parameters.Add(id);

            using (var reader = command.ExecuteReader())
            {
                // 읽을 수 없으면 결과값을 찾을 수 없으므로 예외 출력
                if (reader.Read())
                {
                    SetData(reader);
                }
                else
                {
                    throw new DatabaseDataException();
                }
            }
        }
        static public List<DatabaseData> SelectTeam(MySqlConnection connection, string searchText)
        {
            MySqlParameter team = new MySqlParameter("@team", MySqlDbType.Text);
            team.Value = searchText;

            MySqlCommand command = new MySqlCommand(@"
select * from `ckgame`.`ckgame_mckinley`
where match(`team`) against(@team)", connection);

            command.Parameters.Add(team);

            List<DatabaseData> datas = new List<DatabaseData>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    datas.Add(new DatabaseData(reader));
                }
                if (datas.Count == 0)
                    throw new DatabaseDataException();
                return datas;
            }
        }
        static public List<DatabaseData> SelectOwner(MySqlConnection connection, string searchText)
        {
            MySqlParameter owner = new MySqlParameter("@owner", MySqlDbType.Text);
            owner.Value = searchText;

            MySqlCommand command = new MySqlCommand(@"
select * from `ckgame`.`ckgame_mckinley`
where match(`owner`) against(@owner)", connection);

            command.Parameters.Add(owner);

            List<DatabaseData> datas = new List<DatabaseData>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    datas.Add(new DatabaseData(reader));
                }
                if (datas.Count == 0)
                    throw new DatabaseDataException();
                return datas;
            }
        }
        T StringToEnum<T>(string value) where T : struct, IConvertible
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
    public class DatabaseDataException : Exception
    {
        public DatabaseDataException() : base() { }
        public DatabaseDataException(string message) : base(message) { }
        public DatabaseDataException(string message, Exception innerException) : base(message, innerException) { }
    }
}
