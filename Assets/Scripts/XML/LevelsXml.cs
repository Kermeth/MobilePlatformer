using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("LevelsList")]
public class LevelsXml {

    [XmlArray("Levels"),XmlArrayItem("Level")]
    public List<Level> levelsList = new List<Level>();

    public void Save(string path) {
        var serializer = new XmlSerializer(typeof(LevelsXml));
        using (var stream = new FileStream(path, FileMode.Create)) {
            serializer.Serialize(stream, this);
        }
    }

    public static LevelsXml Load(string path) {
        var serializer = new XmlSerializer(typeof(LevelsXml));
        using (var stream = new FileStream(path, FileMode.Open)) {
            return serializer.Deserialize(stream) as LevelsXml;
        }
    }

    //Loads the xml directly from the given string. Useful in combination with www.text.
    public static LevelsXml LoadFromText(string text) {
        var serializer = new XmlSerializer(typeof(LevelsXml));
        return serializer.Deserialize(new StringReader(text)) as LevelsXml;
    }

}
