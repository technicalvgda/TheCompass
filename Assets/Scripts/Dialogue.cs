/* Script that makes dialogues appear as typed. Put this script where the Text componenent is */ 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour 
{
	/* Text that will be used for the actual text */ 
	public Text text;

	/* Controls the text speed in seconds */ 
	public float textSpeed = .05f;

	/* Text color controlled via the inspector */ 
	public Color textColor;

	/* Character limit that will be shown on screen before it is rolled over to the next part of the text */ 
	public int charLimit = 500;

	void Start () 
	{
		text = GetComponent<Text> ();
		text.color = new Color(textColor.r, textColor.g, textColor.b);
		StartCoroutine (AnimateText ("Okay, guys, we’ve been in school for a few weeks now, and in case you find your classes to be dull, try a few of these things to liven up class a bit.  1. Smoke a pipe and respond to each point the professor makes by waving it and saying, “Quite right, old bean!” 2. Wear X-Ray Specs. Every few minutes, ask the professor to focus the overhead projector. 3. Sit in the front row and spend the lecture filing your teeth into sharp points. 4. Sit in the front and color in your textbook. 5. When the professor calls your name in roll, respond “that’s my name, don’t wear it out!” 6. Introduce yourself to the class as the “master of the pan flute”. 7. Give the professor a copy of The Watchtower. Ask him where his soul would go if he died tomorrow. 8. Wear earmuffs. Every few minutes, ask the professor to speak louder. 9. Leave permanent markers by the dry-erase board. 10. Squint thoughtfully while giving the professor strange looks. In the middle of lecture, tell him he looks familiar and ask whether he was ever in an episode of Starsky and Hutch. 11. Ask whether the first chapter will be on the test. If the professor says no, rip the pages out of your textbook. 12. Become entranced with your first physics lecture, and declare your intention to pursue a career in measurements and units. 13. Sing your questions. 14. Speak only in rhymes and hum the Underdog theme. 15. When the professor calls roll, after each name scream “THAT’S MEEEEE! Oh, no, sorry.” 16. Insist in a Southern drawl that your name really is Wuchen Li. If you actually are Chinese, insist that your name is Vladimir Fernandez O’Reilly. 17. Page through the textbook scratching each picture and sniffing it. 18. Wear your pajamas. Pretend not to notice that you’ve done so. 19. Hold up a piece of paper that says in large letters “CHECK YOUR FLY”. 20. Inform the class that you are Belgian royalty, and have a friend bang cymbals together whenever your name is spoken. 21. Stare continually at the professor’s crotch. Occassionally lick your lips. 22. Address the professor as “your excellency”. 23. Sit in the front, sniff suspiciously, and ask the professor if he’s been drinking. 24. Shout “WOW!” after every sentence of the lecture. 25. Bring a mirror and spend the lecture writing Bible verses on your face. 26. Ask whether you have to come to class. 27. Present the professor with a large fruit basket. 28. Bring a “seeing eye rooster” to class. 29. Feign an unintelligible accent and repeatedly ask, “Vet ozzle haffen dee henvay?” Become aggitated when the professor can’t understand you. 30. Relive your Junior High days by leaving chalk stuffed in the chalkboard erasers. 31. Watch the professor through binoculars. 32. Start a “wave” in a large lecture hall. 33. Ask to introduce your “invisible friend” in the empty seat beside you, and ask for one extra copy of each handout. 34. When the professor turns on his laser pointer, scream “AAAGH! MY EYES!” 35. Correct the professor at least ten times on the pronunciation of your name, even it’s Smith. Claim that the i is silent. 36. Sit in the front row reading the professor’s graduate thesis and snickering. 37. As soon as the first bell rings, volunteer to put a problem on the board. Ignore the professor’s reply and proceed to do so anyway. 38. Claim that you wrote the class text book. 39. Claim to be the teaching assistant. If the real one objects, jump up and scream “IMPOSTER!” 40. Spend the lecture blowing kisses to other students. 41. Every few minutes, take a sheet of notebook paper, write “Signup Sheet #5” at the top, and start passing it around the room. 42. Stand to ask questions. Bow deeply before taking your seat after the professor answers. 43. Wear a cape with a big S on it. Inform classmates that the S stands for “stud”. 44. Interrupt every few minutes to ask the professor, “Can you spell that?” 45. Disassemble your pen. “Accidently” propel pieces across the room while playing with the spring. Go on furtive expeditions to retrieve the pieces. Repeat. 46. Wink at the professor every few minutes. 47. In the middle of lecture, ask your professor whether he believes in ghosts. 48. Laugh heartily at everything the professor says. Snort when you laugh. 49. Wear a black hooded cloak to class and ring a bell. 50. Ask your math professor to pull the roll chart above the blackboard of ancient Greek trade routes down farther because you can’t see Macedonia. ", charLimit));
	}

	/* Animates the text as if it were scrollling. textInput is the text that will be used and charactersOnScreen will determine how many characters will be shown on screen before it is rolled over */ 
	IEnumerator AnimateText(string textInput, int charactersOnScreen) 
	{
		int i = 0;
		int currentNumberOfCharacters = 0;
		text.text = "";
		while(i < textInput.Length)
		{
			if(currentNumberOfCharacters > charactersOnScreen) {
				text.text = "";
				currentNumberOfCharacters = 0;
			}
			text.text += textInput [i++];
			currentNumberOfCharacters++;
			yield return new WaitForSeconds (textSpeed);
		}
	}
}
