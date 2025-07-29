using Godot;
using hundun.unitygame.gamelib;
using System;

namespace hundun.unitygame.enginecorelib
{
    public class GodotFrontend : IFrontend
    {
        public string[] fileGetChilePathNames(string folder)
        {
            throw new NotImplementedException();
        }

        public string fileGetContent(string file)
        {
            throw new NotImplementedException();
        }

        public void log(string logTag, string format)
        {
            GD.Print(JavaFeatureForGwt.stringFormat("[{0}] {1}", logTag, format));
        }
    }
}
