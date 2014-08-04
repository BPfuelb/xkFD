import processing.core.*; 
import processing.data.*; 
import processing.event.*; 
import processing.opengl.*; 

import ddf.minim.analysis.BeatDetect; 
import ddf.minim.*; 

import java.util.HashMap; 
import java.util.ArrayList; 
import java.io.File; 
import java.io.BufferedReader; 
import java.io.PrintWriter; 
import java.io.InputStream; 
import java.io.OutputStream; 
import java.io.IOException; 

public class LevelGenerator extends PApplet {

/**
 *  LevelGenerator f\u00fcr das Spiel xkfd
 *  Eine Musik-Datei kann ausgewaehlt werden, um es in eine 
 *  Wav-Datei umzuwandeln und gleichzeitig zu analysieren.
 *  Fuer jeden Beat wird in einer Text-Datei der Millisekunden-
 *  Wert gespeichert.
 *
 *  Author:  Sebastian Schulz
 *  2014 - 08 - 04
 */





Minim minim;  
AudioPlayer input;
AudioRecorder recorder;
AudioMetaData meta;
int position, dauer, mindestDauer;
String filename;

BeatDetect beat;

PrintWriter output;

int[] beats;
int index;
boolean gestartet;
int millistart;
int status;
final static int OK = 0;
final static int UNGUELTIG = 1;
final static int ZUKURZ = 2;

////// GUI
PImage hintergrund;
PFont font48, font32, font24, font12;
int punkte = 0;

public void setup()
{
  size(439, 146);
  
  //////  GUI init
  hintergrund = loadImage("hintergrund.jpg");
  font48 = loadFont("ComicJensFreePro-48.vlw");
  font32 = loadFont("ComicJensFreePro-32.vlw");
  font24 = loadFont("ComicJensFreePro-24.vlw");
  font12 = loadFont("ComicJensFreePro-12.vlw");
  textFont(font32, 32);

  frameRate(44000);
  
  //////  Beat-Detection init
  beats = new int[10];
  index = 0;
  gestartet = false;
  millistart = 0;
  status = OK;

  minim = new Minim(this);

  position = 0;
  dauer = 0;
  mindestDauer = 10000;  //  10 Sekunden
  filename = "analyse";
  
  beat = new BeatDetect();
  
  //////  L\u00e4rmenergiemodus:
  beat.detectMode(BeatDetect.SOUND_ENERGY);
  noLoop();
}

public void draw()
{
  if (!gestartet)
  {
    image(hintergrund, 0, 0);
    fill(0);
    textAlign(LEFT, TOP);
    switch(status)
    {
      case OK:
        textFont(font32, 32);
        text("Bitte Lied waehlen", 50, 24);
        textFont(font12, 12);
        text("Je kuerzer das Lied desto kuerzer das Level", 51, 63);
        los();
        break;
      case UNGUELTIG:
        textFont(font32, 32);
        text("Falscher Dateityp.", 50, 24);
        textFont(font12, 12);
        text("Erlaubt sind WAV-, AIFF-, AU-, SND-, und MP3-Dateien.", 51, 63);
        los();
        break;
      case ZUKURZ:
        textFont(font32, 32);
        text("Lied zu kurz.", 50, 24);
        textFont(font12, 12);
        text("Das Lied muss mindestens " + mindestDauer + " Sekunden lang sein.", 51, 63);
        los();
        break;
    }
    
  }
  
  if (gestartet && input != null && input.isPlaying())
  {
    //////  Nur jeden 1000sten Frame zeichnen 
    if (frameCount % 1000 == 0)
    {
      image(hintergrund, 0, 0);
      //fill(0);
      textFont(font32, 32);
      textAlign(LEFT, TOP);
      text("Lied wird analysiert", 50, 47);
      for(int i = 0; i < punkte; i++){
        text(".", 345 + i*10, 47);
      }
      textFont(font24, 24);
      textAlign(LEFT, BOTTOM);
      position = PApplet.parseInt(map(input.position(), 0, dauer, 0, 340));
      for(int i = 0; i < position; i+=5){
        text("|", 49 + i, 127);
      }
    }
      
    beat.detect(input.mix);
  
    
    //////  Beat
    if (beat.isOnset()) {
      //println("Beat");
      
      //output.println(millis());
      beats[index] = millis() - millistart;
      if(index > 0 && beats[index] - beats[index-1] > 500)
      {
        index++;
        punkte = (punkte + 1)%4;
        //println(millis());
        chkArray();
      }
      else if(index < 1)
      {
        index++;
        //println(millis()); 
        chkArray();
      }

    }
    
  }
  //////  Wenn Lied zu Ende, Programm schliessen
  if(input != null && gestartet && input.position() > 10 && !input.isPlaying())
  {
    ende();
  }
}


//////  chkArray()
//  erweitert bei Bedarf das Array
public void chkArray() {
  if(index == beats.length)
  {
    //println("Array vergroessern");
    println(frameRate);
    int[] neuesArray = new int[beats.length * 2];
    
    for(int i = 0; i < beats.length; i++)
    {
      neuesArray[i] = beats[i];
    }
    
    beats = neuesArray;
  }
}


//////  los()
//  startet das Datei-Waehl-Fenster
public void los()
{
  selectInput("Datei waehlen:", "dateiGewaehlt");
}


////// ende()
//  schreibt die gesammelten Informationen in die Datei,
//  schliesst txt- und sound-Dateien,
//  erzeugt eine fertig-Datei, wenn erfolgreich Datei erzeugt
public void ende() {
  recorder.endRecord();
  recorder.save();
  
  input.close();
  
  minim.stop();
  super.stop();
  
  
  ////// Neue txt-Datei erzeugen
  output = createWriter(filename + ".txt");
  
  ////  Laenge des Liedes in Millisekunden in erste Zeile schreiben
  output.println(dauer); 
  
  ////  Anzahl der registrierten Beats in zweite Zeile schreiben, danach Leerzeile
  output.println(index);
  
  output.println();
  
  ////  Beats-Array in Datei schreiben
  for(int i = 0; i < index; i++)
  {
    output.println(beats[i]);
  }
  
  output.flush(); // Writes the remaining data to the file
  output.close(); // Finishes the file
  
  println(index + " Beats in Datei geschrieben.");
  
  
  ////  fertig-Datei erzeugen
  output = createWriter("fertig.txt");
  output.println(index + " Beats in Datei geschrieben.");
  output.flush();
  output.close();
  
  
  println("exit");
  exit(); // Stops the program
}


////// dateiGewaehlt()
//  wenn Datei-Waehl-Dialog geschlossen wurde
public void dateiGewaehlt(File auswahl)
{
  if (auswahl == null)
  {
    println("Fenster wurde geschlossen oder der Benutzer hat Cancel " 
          + "geklickt. Abbruch.");
    
    ////  fertig-Datei erzeugen
    output = createWriter("abbruch.txt");
    output.println("Fenster wurde geschlossen oder "
          + "der Benutzer hat Cancel geklickt.");
    output.flush();
    output.close();
  
    println("cancel");
    exit();
  }
  else if (auswahl.exists() && auswahl.isFile() && 
      (
        auswahl.getName().toLowerCase().endsWith("mp3") || 
        auswahl.getName().toLowerCase().endsWith("wav") || 
        auswahl.getName().toLowerCase().endsWith("aiff") || 
        auswahl.getName().toLowerCase().endsWith("au") || 
        auswahl.getName().toLowerCase().endsWith("snd")
      )
     )
  {
    status = OK;
    
    println("User selected " + auswahl.getAbsolutePath());
    input = minim.loadFile(auswahl.getAbsolutePath());
    meta = input.getMetaData();
    dauer = input.length();
    if (dauer < mindestDauer)
    {
      status = ZUKURZ;
      redraw();
      return;
    }
    
    ////  Neuen Rekorder erzeugen
    recorder = minim.createRecorder(input, filename + ".wav", true);
    
    gestartet = true;
    millistart = millis();
    loop();
    input.play();
    recorder.beginRecord();
  }
  else
  {
    println("Die Datei ist nicht vom richtigen Typ.");
    
    status = UNGUELTIG;
    redraw();
    
    //los();
    //exit();
  }
}
  static public void main(String[] passedArgs) {
    String[] appletArgs = new String[] { "LevelGenerator" };
    if (passedArgs != null) {
      PApplet.main(concat(appletArgs, passedArgs));
    } else {
      PApplet.main(appletArgs);
    }
  }
}
