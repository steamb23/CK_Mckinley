using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK_Mckinley
{
    public enum DBGenre
    {
        ETC,
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
        ETC,
        _2D,
        _3D,
    }
    public enum DBPlatform
    {
        ETC,
        PC,
        Mobile,
        Web,
        XBOX,
        PS,
        Nintendo
    }
    public enum DBYear
    {
        _2015,
        _2014,
        _2013,
        _2012,
        _2011,
        _2010,
        _2009,
        _2008,
        _2007,
        _2006,
        _2005,
        _2004,
        _2003,
        _2002,
        _2001,
        _2000,
        _1999
    }
    public enum DBWorld
    {
        ETC,
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
        ETC,
        DirectX,
        Unity,
        Unreal,
        Gamebryo,
        Cocos2Dx,
    }
    public enum DBResourceType
    {
        ETC,
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
    class DatabaseData
    {
        public static string EnumToDBString(Enum e)
        {
            return e.ToString().ToLower().Replace("_", "");
        }
        public static object DBStringToEnum(Type enumType, string value)
        {
            // 첫글자에 숫자가 있으면.
            if (value.First() < '0' || value.First() > '9')
                value = "_" + value;
            return Enum.Parse(enumType, value, true);
        }
    }
}
