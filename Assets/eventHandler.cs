using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class choice
{   
    public string choice_text;
    public string result_text;
    public int money;
    public int happiness;
    public int popularity;
}

public class eventData
{
    public choice greenChoice;
    public choice blueChoice;
    public choice redChoice;

    public bool skip = false; // Already used, skip this one

    public Sprite Image;
    public string Title;
    public string Text;

    public eventData()
    {
        greenChoice = new choice();
        blueChoice = new choice();
        redChoice = new choice();
    }

    public void pressGreen()
    {
        staticData.ChoiceHasBeenMade = true;
        staticData.HappyStatus += greenChoice.happiness;
        staticData.MoneyStatus += greenChoice.money;
        staticData.PopularityStatus += greenChoice.popularity;
    }

    public void pressBlue()
    {
        staticData.ChoiceHasBeenMade = true;
        staticData.HappyStatus += blueChoice.happiness;
        staticData.MoneyStatus += blueChoice.money;
        staticData.PopularityStatus += blueChoice.popularity;
    }

    public void pressRed()
    {
        staticData.ChoiceHasBeenMade = true;
        staticData.HappyStatus += redChoice.happiness;
        staticData.MoneyStatus += redChoice.money;
        staticData.PopularityStatus += redChoice.popularity;
    }
}

public class eventHandler : MonoBehaviour
{
    private int choice;

    public Sprite[] workImages;
    public Sprite[] funImages;
    public Sprite[] popularityImages;

    public GameObject eventTitle;
    public GameObject eventText;
    public GameObject resultText;
    public GameObject eventImage;
    public GameObject choice1;
    public GameObject choice2;
    public GameObject choice3;

    private List<eventData> workEvents = new List<eventData>();
    private List<eventData> funEvents = new List<eventData>();
    private List<eventData> popularityEvents = new List<eventData>();
    public eventData currentEvent;

    // Start is called before the first frame update
    void Start()
    {
        // Create all events       
        createEvents();
        eventData ev = new eventData();

        switch (staticData.Field1)
        {
            case 1:
                ev = getEvent("fun");
                break;
            case 2:
                ev = getEvent("popularity");
                break;
            case 3:
                ev = getEvent("work");
                break;
            default:
                Debug.Log("Error: Invalid field value in staticdata.field1");
                break;
        }
        populateInterface(ev);
    }

    public void populateInterface(eventData ev)
    {
        currentEvent = ev;
        TextMeshProUGUI title = eventTitle.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI text = eventText.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI c1 = choice1.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI c2 = choice2.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI c3 = choice3.GetComponent<TextMeshProUGUI>();
        SpriteRenderer img = eventImage.GetComponent<SpriteRenderer>();

        title.text = ev.Title;
        text.text = ev.Text;
        c1.text = ev.greenChoice.choice_text;
        c2.text = ev.blueChoice.choice_text;
        c3.text = ev.redChoice.choice_text;
        img.sprite = ev.Image;
    }

    public void progressLife()
    {
        staticData.LifeProgress++;
        if (staticData.LifeProgress > 18)
        {
            SceneManager.LoadScene(sceneName: "EndScreen");
        }

        int fieldType = getFieldType();
        string fieldTypeString = "";
        switch(fieldType)
        {
            case 1:
                fieldTypeString = "fun";
                break;
            case 2:
                fieldTypeString = "popularity";
                break;
            case 3:
                fieldTypeString = "work";
                break;
        }
        staticData.ChoiceHasBeenMade = false;
        eventData ev = getEvent(fieldTypeString);
        populateInterface(ev);
    }

    private int getFieldType()
    {
        int currentField = (staticData.LifeProgress + 1) / 2;
        int retValue = 0;
        switch(currentField)
        {   
            case 1:
                retValue = staticData.Field1;
                break;
            case 2:
                retValue = staticData.Field2;
                break;
            case 3:
                retValue = staticData.Field3;
                break;
            case 4:
                retValue = staticData.Field4;
                break;
            case 5:
                retValue = staticData.Field5;
                break;
            case 6:
                retValue = staticData.Field6;
                break;
            case 7:
                retValue = staticData.Field7;
                break;
            case 8:
                retValue = staticData.Field8;
                break;
            case 9:
                retValue = staticData.Field9;
                break;
            default:
                retValue = 0;
                Debug.Log("Error in getfieldtype. Returned 0");
                break;
        }
        return retValue;
    }

    public eventData getEvent(string type)
    {
        int r = 0;
        bool foundNewEvent = false;
        eventData ev = new eventData();
            switch (type)
            {
                case "work":
                    while (!foundNewEvent)
                    {
                        r = UnityEngine.Random.Range(0, workEvents.Count);
                        ev = workEvents[r];
                        if (ev.skip == false)
                        {
                            foundNewEvent = true;
                            workEvents[r].skip = true;
                        }
                    }
                    break;
                case "fun":
                    while (!foundNewEvent)
                    {
                        r = UnityEngine.Random.Range(0, funEvents.Count);
                        ev = funEvents[r];
                        if (ev.skip == false)
                        {
                            foundNewEvent = true;
                            funEvents[r].skip = true;
                        }
                    }
                    break;
                case "popularity":
                    while (!foundNewEvent)
                    {
                        r = UnityEngine.Random.Range(0, popularityEvents.Count);
                        ev = popularityEvents[r];
                        if (ev.skip == false)
                        {
                            foundNewEvent = true;
                            popularityEvents[r].skip = true;
                        }
                    }
                    break;
        }
        return ev;
    }

    private void createEvents()
    {
        // Work #1
        eventData ev = new eventData();
        ev.Title = "Your boss approaches";
        ev.Text = "We are going to go ahead and have you work longer hours for the same pay.";
        ev.Image = workImages[3];

        ev.greenChoice.choice_text = "I'll work even longer than that!";
        ev.greenChoice.result_text = "You work long hours and get noticed. You get that raise you dreamed about. But you are tired all the time.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "I quit!";
        ev.redChoice.result_text = "What a relief! You are free of that terrible job! But your income suffers.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "Allright, boss.";
        ev.blueChoice.result_text = "You work long hours with nothing to show for it.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #2
        ev = new eventData();
        ev.Title = "The copier exploded!";
        ev.Text = "Everyone is looking at you!";
        ev.Image = workImages[2];

        ev.greenChoice.choice_text = "'I didnt think the cat picture was THAT funny!'";
        ev.greenChoice.result_text = "Everyone laughs, you get away with it!";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "'Friggin piece of muthaluvin poop!'";
        ev.redChoice.result_text = "The boss was walking by. You get a stern talking to!";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "Clean it up the best you can, then have IT order a new one.";
        ev.blueChoice.result_text = "A new copier arrives. Your coworkers appreciate the cleanup attempt.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        workEvents.Add(ev);

        // Work #3
        ev = new eventData();
        ev.Title = "The TPS report";
        ev.Text = "You forgot to file the TPS report!";
        ev.Image = workImages[2];

        ev.greenChoice.choice_text = "Hide it in the trashcan and hope the boss forgets.";
        ev.greenChoice.result_text = "Hey, umm, about that TPS report I asked for... yeah... if you could go ahead and redo that...";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Sneak it into the TPS pile on the boss's desk.";
        ev.redChoice.result_text = "The boss never notices it was gone.. But he will need a copy of it again next wednesday, mmmkay?";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "Ask your boss 'Who reads those anyway?'";
        ev.blueChoice.result_text = "Your boss can't answer that. Next week they stop asking for TPS reports.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        workEvents.Add(ev);

        // Work #4
        ev = new eventData();
        ev.Title = "You are delayed";
        ev.Text = "A tire on your car has fallen off.";
        ev.Image = workImages[5];

        ev.greenChoice.choice_text = "Call triple A. Get that tire fixed and get to work ASAP!";
        ev.greenChoice.result_text = "You get into the office just in time to realize it's saturday.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Walk home. That job was boring anyway, You'll fix the car tomorrow.";
        ev.redChoice.result_text = "You play games all day. Nobody realizes you were gone.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You walk to work and call a mechanic to fix the car and drive it to you.";
        ev.blueChoice.result_text = "Your boss appreciated your dedication to your deskjob. You get a small bonus.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #5
        ev = new eventData();
        ev.Title = "Meetings!";
        ev.Text = "You have meetings scheduled all day.";
        ev.Image = workImages[3];

        ev.greenChoice.choice_text = "Pay attention, answer every question and show up on time, every time.";
        ev.greenChoice.result_text = "Your coworkers think you are brown-nosing. Your boss calls you by your first name!";
        ev.greenChoice.happiness = 0;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "Send an E-Mail titled: 'All those meetings could have been an e-mail!'";
        ev.redChoice.result_text = "Meetings are canceled after a row breaks out over the point of the meetings. Your coworkers are happy to skip the meetings.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You call in sick and get the meeting minutes later.";
        ev.blueChoice.result_text = "Your boss realizes why you called in sick, he sends you a stern letter.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #6
        ev = new eventData();
        ev.Title = "Bonus day!";
        ev.Text = "You get a sizeable bonus for a job well done.";
        ev.Image = workImages[4];

        ev.greenChoice.choice_text = "I'll save it for old age.";
        ev.greenChoice.result_text = "Your retirement fund has grown larger. It makes you happy.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "I'll blow it all on partying.";
        ev.redChoice.result_text = "You are broke and have a hangover. Karen from the office did not appreciate your WW2 jokes.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "I'll buy a pool for the family in time for the holidays!";
        ev.blueChoice.result_text = "Your family is happy with the new pool!";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        workEvents.Add(ev);

        // Work #7
        ev = new eventData();
        ev.Title = "Just another boring workday.";
        ev.Text = "You have counted the ceiling tiles fifty times today.";
        ev.Image = workImages[3];

        ev.greenChoice.choice_text = "Better find something to do!";
        ev.greenChoice.result_text = "You start answering the phones. Now you do your job and answer the phones in the future. yay..";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Time for indoor paper golfing!";
        ev.redChoice.result_text = "You broke Karen's family photo frame. Whoops! But it was fun!";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Better count them again!";
        ev.blueChoice.result_text = "Your brain tries to escape your skull due to utter boredom.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #8
        ev = new eventData();
        ev.Title = "Big fish.";
        ev.Text = "You are charged with showing the new client the company.";
        ev.Image = workImages[1];

        ev.greenChoice.choice_text = "You show him everything nice about the company, and then go out for pizza. ";
        ev.greenChoice.result_text = "The client is happy at the end of the day. Your boss appreciates this.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You show him the seedy side of the company, including the secret sleeping area the boss does not know about.";
        ev.redChoice.result_text = "The client seems mildly amused. Your coworkers are not happy when the sleeping area is taken away.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You ditch the company building and take the client out drinking.";
        ev.blueChoice.result_text = "The client offers you a better paying job than your current one. You take it!";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #9
        ev = new eventData();
        ev.Title = "The secratary.";
        ev.Text = "A new secretary starts at the company. You think she might like you.";
        ev.Image = workImages[6];

        ev.greenChoice.choice_text = "You compliment her work ethics after a couple of weeks.";
        ev.greenChoice.result_text = "You become friends.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You send her some... pictures...";
        ev.redChoice.result_text = "You are reported to HR and spend four weeks in sexual harassment seminars.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Ignore her. Just another face to see every day.";
        ev.blueChoice.result_text = "Your boss is happy with your focus on working.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #10
        ev = new eventData();
        ev.Title = "A private toilet!";
        ev.Text = "One day you discover a pristine toilet in an unused room in the building.";
        ev.Image = workImages[7];

        ev.greenChoice.choice_text = "You share the private toilet with a select group of friends.";
        ev.greenChoice.result_text = "Everyone seems a little bit happier at work after this.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "Defile the toilet.";
        ev.redChoice.result_text = "The smell attracts your coworkers after a few weeks. You are discovered to be the culprit.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You spent a few glorious weeks alone with the toilet.";
        ev.blueChoice.result_text = "You are happy with your private paradise.";
        ev.blueChoice.happiness = 2;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #11
        ev = new eventData();
        ev.Title = "Pizza party!";
        ev.Text = "The boss brings pizzas!";
        ev.Image = workImages[0];

        ev.greenChoice.choice_text = "Eat a slice or two. You mingle with your coworkers.";
        ev.greenChoice.result_text = "You make a couple of new acquaintances. And you got pizza. Score!";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "Eat ALL the pizza!";
        ev.redChoice.result_text = "You eat most of the pizza. Your coworkers go hungry and give you angry looks. You dont have to eat for a couple of days.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 1;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Ignore the pizza, you're on Keto!";
        ev.blueChoice.result_text = "You tell everyone about the message of Keto. Again, and again.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        workEvents.Add(ev);

        // Work #12
        ev = new eventData();
        ev.Title = "Your chair squeaks.";
        ev.Text = "You notice if you move just right your chair makes a sqeaky sound.";
        ev.Image = workImages[6];

        ev.greenChoice.choice_text = "Make a joke about the squeak and then squak the chair for comedic effect.";
        ev.greenChoice.result_text = "Bob laughs. Ahh, office humor.";
        ev.greenChoice.happiness = 0;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You sqeak the chair constantly.";
        ev.redChoice.result_text = "People are annoyed with you, but eventually your boss buys a new chair for you. Score!";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You get some oil and fix the squeak.";
        ev.blueChoice.result_text = "You feel like this could have been funny somehow. Meh.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #13
        ev = new eventData();
        ev.Title = "No internet!";
        ev.Text = "There is no internet today.";
        ev.Image = workImages[3];

        ev.greenChoice.choice_text = "Make do. You have most of your work files on your computer anyway.";
        ev.greenChoice.result_text = "Your boss appreciates your hustle.";
        ev.greenChoice.happiness = 0;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You yell 'No internet! No work!' and run out of the building.";
        ev.redChoice.result_text = "Some coworkers manage to successfully convince your boss that no internet indeed means no work. Everyone goes home.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You get on the phone with your ISP and get the internet back online.";
        ev.blueChoice.result_text = "You are noticed for your initiative. You get a promotion!";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #14
        ev = new eventData();
        ev.Title = "Office workout.";
        ev.Text = "Sarah watched a TV show about office workout team building. So the office is now doing office workout team building.";
        ev.Image = workImages[6];

        ev.greenChoice.choice_text = "Run faster than anyone else! Push more! Do MORE!";
        ev.greenChoice.result_text = "You get arrested after you pushed over a flagpole and screamed at a kid.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "You sneak away from the group to chill at a nearby shop. The shop owner tells you to buy something or move on, so you buy a lottery ticket.";
        ev.redChoice.result_text = "Nobody notices you were gone, you win big on the lottery ticket!";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 2;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You get on the phone with your ISP and get the internet back online.";
        ev.blueChoice.result_text = "You are noticed for your initiative. You get a promotion!";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #15
        ev = new eventData();
        ev.Title = "The startup.";
        ev.Text = "Sam the software developer has a startup idea and offers you to join him.";
        ev.Image = workImages[5];

        ev.greenChoice.choice_text = "Yes!";
        ev.greenChoice.result_text = "The company goes bust and you have to crawl back to your old job, but people admire your bravery for doing this.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "No!";
        ev.redChoice.result_text = "Sam strikes it big. You wonder what might have been.";
        ev.redChoice.happiness = -2;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "As long as there is a foosball table.";
        ev.blueChoice.result_text = "You and Sam manage to make a nice profit of your work. You sell it to a competitor and return to your old job.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 2;
        ev.blueChoice.popularity = 0;

        workEvents.Add(ev);

        // Work #16
        ev = new eventData();
        ev.Title = "Liquidation.";
        ev.Text = "Your company is being liquidated. You are fired.";
        ev.Image = workImages[7];

        ev.greenChoice.choice_text = "You decide to pursue your dream, game development!";
        ev.greenChoice.result_text = "After a few lonely months without sales you return to a similar deskjob as before.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Leave a parting 'gift' on your boss's table.";
        ev.redChoice.result_text = "Your boss never realizes who did it. But you know, oh you know.";
        ev.redChoice.happiness = 2;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You will leave when you are ready to leave! Nobody fires you, dangit!";
        ev.blueChoice.result_text = "A month passes until the new company in the building notices you hiding under your desk. You are escorted out.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = -1;

        workEvents.Add(ev);

        // Work #17
        ev = new eventData();
        ev.Title = "Office gunman!";
        ev.Text = "Brian the mail sorter snaps and brings a gun to work!";
        ev.Image = workImages[0];

        ev.greenChoice.choice_text = "You become a hostage and do as he says. You survive until he gives up.";
        ev.greenChoice.result_text = "After a few lonely months without sales you return to a similar deskjob as before.";
        ev.greenChoice.happiness = 0;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Tackle Brian and get that gun off him!";
        ev.redChoice.result_text = "Success! You are hailed as a hero by your coworkers but your boss is angry that you risked it due to company insurance.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 2;

        ev.blueChoice.choice_text = "You try sneaking out to get help.";
        ev.blueChoice.result_text = "Brian sees you and you get injured. You spend a couple of months at the hospital.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = 1;

        workEvents.Add(ev);

        // Work #18
        ev = new eventData();
        ev.Title = "The job offer.";
        ev.Text = "A headhunter contacts you and offers you twice your current pay if you take the job offer.";
        ev.Image = workImages[4];

        ev.greenChoice.choice_text = "You take him up on the offer! Twice the money! Wow!";
        ev.greenChoice.result_text = "It turns out to be a piramid scheme. You lose a lot of money and end up begging for youd old job back.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -2;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You leverage the offer to get a raise.";
        ev.redChoice.result_text = "Your boss tells you you are welcome to leave. You did not see that coming, and he's not happy.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You turn down the offer and notify your boss people at the company are being headhunted.";
        ev.blueChoice.result_text = "The offer turns out to be a piramid scheme. Your boss and coworkers are happy with your dedication.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 1;

        workEvents.Add(ev);

        // fun #1
        ev = new eventData();
        ev.Title = "A friend calls";
        ev.Text = "Hey dude. We are starting a surfing club. You in?";
        ev.Image = funImages[0];

        ev.greenChoice.choice_text = "Of-friggin-course! Beer's on me, dude!";
        ev.greenChoice.result_text = "Surf's up!";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "No! I'm not made of free time!";
        ev.redChoice.result_text = "You sit at home and look at your friends wasting time at the beach. Hahh, slackers!";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Surfing? Let's start with something a bit less risky.";
        ev.blueChoice.result_text = "You join a local metal detecting club instead.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        funEvents.Add(ev);

        // fun #2
        ev = new eventData();
        ev.Title = "The band";
        ev.Text = "A old mate of yours offers you a position in his band.";
        ev.Image = funImages[7];

        ev.greenChoice.choice_text = "'Sure, Let's hit the road.'";
        ev.greenChoice.result_text = "You play gigs regularly and become local celebraties. Too bad the paparazi are so aggressive.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 2;

        ev.redChoice.choice_text = "'Fiiine. I'll join...'";
        ev.redChoice.result_text = "You audition for the band but are turned down. They weren't any good anyway.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "'Sure, I'll join! Let me bring my bongo's.'";
        ev.blueChoice.result_text = "You spend every saturday playing music for drunk college students. You find it fun!";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        funEvents.Add(ev);

        // fun #3
        ev = new eventData();
        ev.Title = "Vacation";
        ev.Text = "You have some vacation saved up. Maybe its time to use it.";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "You decide to visit europe. You've always wanted to go.";
        ev.greenChoice.result_text = "You visit 'the europe' and embarass yourself with your lack of geography knowledge.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You spend two weeks in a near constant state of partying.";
        ev.redChoice.result_text = "You end up happy, but man, where did all your money go?";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -2;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You decide a day or two at a nice beach are in order.";
        ev.blueChoice.result_text = "The two days stretch into a week. You had a relaxing and fun time.";
        ev.blueChoice.happiness = 2;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        funEvents.Add(ev);

        // fun #4
        ev = new eventData();
        ev.Title = "The seance";
        ev.Text = "You go to a seance, maybe you'll contact Jimmy Hoffa.";
        ev.Image = funImages[5];

        ev.greenChoice.choice_text = "You participate and play along with everything that goes on.";
        ev.greenChoice.result_text = "You had a surprisingly good time, and you made a couple of new friends from the attending people.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You decide to spice things up!";
        ev.redChoice.result_text = "The host was not happy with your toilet paper mummy impression.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You get engrossed in the seance and try to contact Hoffa.";
        ev.blueChoice.result_text = "You are surprised when some guy named Dave answers. Turns out he's pretty ok for a ghost.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        funEvents.Add(ev);

        // fun #5
        ev = new eventData();
        ev.Title = "The lemonade";
        ev.Text = "You decide that your new lemonade recipe needs to be tasted by the world!";
        ev.Image = funImages[3];

        ev.greenChoice.choice_text = "You convince a friend to stock your lemonade on his small shop's shelves.";
        ev.greenChoice.result_text = "It turns out that people like your lemonade. You sell a decent amount and turn a small profit.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You spike it with vodka and sell it at a nearby house party.";
        ev.redChoice.result_text = "Your lemonade is a hit with the young adults. You get orders for more.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 1;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You build a small stall and sell it at the next farmers market.";
        ev.blueChoice.result_text = "A big lemonade brand offers to buy your recipe. You say yes!";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 2;
        ev.blueChoice.popularity = 0;

        funEvents.Add(ev);

        // fun #6
        ev = new eventData();
        ev.Title = "The hole in one";
        ev.Text = "You manage to hit a hole in one at the local golf course.";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "Tell all your friends about it!";
        ev.greenChoice.result_text = "Your friends say 'Pics or it didnt happen!. If only you had a camera.' ";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "Throw the golf club after it, yell at the caddy and demand free drinks.";
        ev.redChoice.result_text = "It turns out people are not happy about this if you snuck onto the gold course without paying.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = -2;

        ev.blueChoice.choice_text = "Quetly celebrate, do a small fistpump and go about your day.";
        ev.blueChoice.result_text = "A small group of business men saw your achievement and offer to buy you drinks.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 1;

        funEvents.Add(ev);

        // fun #7
        ev = new eventData();
        ev.Title = "The drive in";
        ev.Text = "You go to the movies! A drive in movie in fact.";
        ev.Image = funImages[2];

        ev.greenChoice.choice_text = "You choose a romantic comedy.";
        ev.greenChoice.result_text = "Your wife is impressed with how in touch with your feelings you are after you cry over the movie.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You choose a horror slasher film.";
        ev.redChoice.result_text = "You are mid-gulp when, suddenly, there is a jump scare! You will need to pay to clean your car after that. So sticky...";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "A foreign language film is just the ticket.";
        ev.blueChoice.result_text = "You sit in bewilderment for two hours. Afterwards you bump into a collegue who is impressed with your taste in movies.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        funEvents.Add(ev);

        // fun #8
        ev = new eventData();
        ev.Title = "A lazy sunday";
        ev.Text = "You put up a hammock and decide to nap in it.";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "Stay still and float to sleep.";
        ev.greenChoice.result_text = "Who knew hammocks could so great for naps! You go on a hammock buying spree.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Pretend you are in an offroad vehicle.";
        ev.redChoice.result_text = "You get caught up in the hammock as it turns over and form a hammock-human caterpillar. The pictures your wife takes go viral.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You decide that a nap is not enough! You decide to spend the night in it.";
        ev.blueChoice.result_text = "You wake up way too late and with a nasty back ache. But at least you are well rested.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = 0;

        funEvents.Add(ev);

        // fun #9
        ev = new eventData();
        ev.Title = "The novel";
        ev.Text = "You decide to write that novel you always wanted to write.";
        ev.Image = funImages[2];

        ev.greenChoice.choice_text = "You write a novel about a murderer on the loose and the detective that eventually catch him.";
        ev.greenChoice.result_text = "Your novel enjoyes local popularity. You come away satisfied.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You write a book about wolves, and dragons, and a BIG wall!";
        ev.redChoice.result_text = "It takes you a few years to write the first part of a whole series of books. Turns out some other wannabe author finished his before you did.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You write a penny dreadful novel.";
        ev.blueChoice.result_text = "It is a hit! You get contacted by a publisher! All the extra work distances you from your friends and family tho.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 2;
        ev.blueChoice.popularity = -1;

        funEvents.Add(ev);

        // fun #10
        ev = new eventData();
        ev.Title = "Couch to 5k";
        ev.Text = "Andy in accounting did couch to 5K. If he did it, so can you!";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "You pace yourself. Running a bit further each day.";
        ev.greenChoice.result_text = "You happen upon a wild cat during one of your runs. Turns out you own a cat now.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "'I'll do it in one day!'";
        ev.redChoice.result_text = "You do your very best. But after a couple of minutes of full speed naruto style running you run out of breath and will to live.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You join a running group.";
        ev.blueChoice.result_text = "You make a few friends and start traveling with them to new places with nice views.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = 1;

        funEvents.Add(ev);

        // fun #11
        ev = new eventData();
        ev.Title = "Zip lining";
        ev.Text = "You read a brochure about zip lining so you decide to give it a try.";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "You follow the instructions to the letter.";
        ev.greenChoice.result_text = "OMG you did not realize anything could be quite this boring!";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "What happens if you jump off mid zip?";
        ev.redChoice.result_text = "You crash into some trees. It hurts but at least you avoided the rest of the zip lining course.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You invite some friends with you.";
        ev.blueChoice.result_text = "Your friends end up disliking you after a couple of hours of boring zip lining.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = -1;

        funEvents.Add(ev);

        // fun #12
        ev = new eventData();
        ev.Title = "The wedding";
        ev.Text = "You come across a wedding party on your daily walk around the neightbourhood.";
        ev.Image = funImages[7];

        ev.greenChoice.choice_text = "You say congratulations to the groom.";
        ev.greenChoice.result_text = "You get invited to the party. You eat lots of cake and make friends. It just feels right to give them a gift in return.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You crash the party for some free cake.";
        ev.redChoice.result_text = "You attempt to lie to the mother of the bride about how you know her from that strip club. You get kicked out and cause a bit of a scandal.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You ask to join the party.";
        ev.blueChoice.result_text = "You are delighted when they say yes. You eat cake and come away happy.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 0;

        funEvents.Add(ev);

        // fun #13
        ev = new eventData();
        ev.Title = "The farm";
        ev.Text = "You spend a day at a local farm helping out.";
        ev.Image = funImages[4];

        ev.greenChoice.choice_text = "You help tend to the horses.";
        ev.greenChoice.result_text = "The farmer lets you feed and ride the horses. It is a fun experience.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You decide to show the cows who's boss.";
        ev.redChoice.result_text = "You try to intimidate the cows into following your orders. They ignore you and a few guys nearby laugh at you.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You herd the chickens.";
        ev.blueChoice.result_text = "The chicken do their best to escape you but you manage to herd them into a coup. The farmer gives you a few eggs in return.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 1;
        ev.blueChoice.popularity = 0;

        funEvents.Add(ev);

        // fun #14
        ev = new eventData();
        ev.Title = "The barbecue";
        ev.Text = "You decide to have a barbecue.";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "You invite your friends over.";
        ev.greenChoice.result_text = "Your friends appreciate the offer and you all have a good day eating BBQ.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You invite no-one. They wouldnt appreciate you special sauce.";
        ev.redChoice.result_text = "You cook 10kg of BBQ meat. You sit alone and eat it all! The hospital bill was not cheap.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You invite your neightbours.";
        ev.blueChoice.result_text = "It turns out a few of your neightbours are drug dealers. A neightbour calls the cops on them a brawl breaks out in your back yard.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = -1;

        funEvents.Add(ev);

        // fun #15
        ev = new eventData();
        ev.Title = "The football game";
        ev.Text = "You go to a football game.";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "You bring a buddy you know like one of the teams that are playing.";
        ev.greenChoice.result_text = "You have a great time at the field, and end up in a pub afterwards to celebrate your teams victory.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You went with a friend who thinks you are a fan of one of the teams.";
        ev.redChoice.result_text = "You have never seen a game of football live, but you find yourself enjoying it.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You go to the game open minded to a new experience.";
        ev.blueChoice.result_text = "It turns out that you do not like football. You did meet some new friends tho.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        funEvents.Add(ev);

        // fun #16
        ev = new eventData();
        ev.Title = "Skydiving";
        ev.Text = "You decide to go skydiving. It looks fun on TV!";
        ev.Image = funImages[1];

        ev.greenChoice.choice_text = "You follow the rules and jump with an instructor.";
        ev.greenChoice.result_text = "It was a blast! You even get a picture of the jump to remember it by.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You decide to jump alone. No tandem jumping for this newbie!";
        ev.redChoice.result_text = "You miss the landing zone by a mile. Eventually you find your way back with the help of some local scouts. They laugh at you the whole way.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You decide to be the last one to jump from the plane.";
        ev.blueChoice.result_text = "Seeing everyone else jump turns your stomach and throw up while on the way down. Your instructor is not happy and you have to pay for his dry cleaning.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = -1;

        funEvents.Add(ev);

        // fun #17
        ev = new eventData();
        ev.Title = "Frisbee golf";
        ev.Text = "A friend gets you to go frisbee golfing with him.";
        ev.Image = funImages[6];

        ev.greenChoice.choice_text = "You stick to the beginner course.";
        ev.greenChoice.result_text = "You manage to finish it in good time. Your friend is impressed with you.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You go for the advanced course.";
        ev.redChoice.result_text = "You spend your day climbing trees, searching through bushes and swimming for your frisbee. Your friend makes fun of you.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You decide to do the medium difficulty course.";
        ev.blueChoice.result_text = "You take quite a long time to finish the course, but feel pretty bad about your performance. Your friend assures you you did fine.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        funEvents.Add(ev);

        // fun #18
        ev = new eventData();
        ev.Title = "Karaeoki";
        ev.Text = "During a pubcrawl you decide to participate in karaeoki.";
        ev.Image = funImages[7];

        ev.greenChoice.choice_text = "You grab the microphone and sing your best rendition of thunderstruck.";
        ev.greenChoice.result_text = "The karaeoki patrons are impressed, since the machine does not have thunderstruck in it.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You belt out your best version of norwegian black metal growling!";
        ev.redChoice.result_text = "The eurovision loving crowd is not impressed.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You do a duo with a old gentleman of Natelie Imbruglia's Torn";
        ev.blueChoice.result_text = "The old man was singing Metallica's Sandman while you sang Torn. You get boo'd.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        funEvents.Add(ev);

        // family #1
        ev = new eventData();
        ev.Title = "You have some free time";
        ev.Text = "You haven't seen your family in a while.";
        ev.Image = popularityImages[0];

        ev.greenChoice.choice_text = "I'll spend some time with them.";
        ev.greenChoice.result_text = "Your family starts a weekly dinner get-together.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "Go to the movies instead.";
        ev.redChoice.result_text = "You lose some touch with your family. But you are on top of the most recent movies!";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "Call mom!";
        ev.blueChoice.result_text = "Your mom is happy to hear from you.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #2
        ev = new eventData();
        ev.Title = "Game night";
        ev.Text = "You pull out some of your board games and invite your siblings over.";
        ev.Image = popularityImages[4];

        ev.greenChoice.choice_text = "Lets play Trivia Pursuit!.";
        ev.greenChoice.result_text = "You end up dead last since your siblings seem to have an unnatural knowledge of sports.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Lets play Monopoly!";
        ev.redChoice.result_text = "A row breaks out over the positioning of a hotel piece at the end of the night.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Lets play Pictionary!";
        ev.blueChoice.result_text = "You show talent drawing up shapes. Your team wins and you celebrate by breaking out some chokolate for everyone.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #3
        ev = new eventData();
        ev.Title = "Family road trip";
        ev.Text = "A family road trip is a great idea, right?";
        ev.Image = popularityImages[6];

        ev.greenChoice.choice_text = "You go for a short trip.";
        ev.greenChoice.result_text = "Your five years old nephew pukes halfway to McDonalds. The smell is unbearable on the way back.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You go for a long trip.";
        ev.redChoice.result_text = "Your journey to a famous landmark takes a couple of days. But it is a great experience and you have good memories of it.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You go for a medium trip.";
        ev.blueChoice.result_text = "Your dad decides to take a dip in a nearby lake. He twists his ankle and the rest of the trip is spent at the hospital.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #4
        ev = new eventData();
        ev.Title = "Visit a museum";
        ev.Text = "A visit to a museum should be fun!";
        ev.Image = popularityImages[6];

        ev.greenChoice.choice_text = "The cardboard museum seems nice.";
        ev.greenChoice.result_text = "It turns out cardboard has a fascinating history. The trip was well worth it.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "The WW2 museum seems interesting.";
        ev.redChoice.result_text = "You spend the day climbing in and out of tanks. The curators have lots to say and you have a blast. Your family however is bored to tears.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "The nearby art museum seems cool.";
        ev.blueChoice.result_text = "The day is spend looking at modern art. You do not get it at all but your niece seems fascinated by it.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #5
        ev = new eventData();
        ev.Title = "Family dinner";
        ev.Text = "You decide to cook for your family!";
        ev.Image = popularityImages[7];

        ev.greenChoice.choice_text = "A baconwrapped hamburger will do the trick.";
        ev.greenChoice.result_text = "The burgers are good, but your vegan sister talks about her lifestyle all night.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "A veggie burger could be delicious.";
        ev.redChoice.result_text = "The burgers are good, but your brother talks about meat all night.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You do your best to cater to all tastes with an italian salat bar complete with meatballs.";
        ev.blueChoice.result_text = "The bar is empty by the time you get to eat. But your family is happy.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #6
        ev = new eventData();
        ev.Title = "Treasure hunt";
        ev.Text = "You organize a family treasure hunt!";
        ev.Image = popularityImages[1];

        ev.greenChoice.choice_text = "You hide things all over your lawn with clues and fun games.";
        ev.greenChoice.result_text = "Everyone has fun! But your lawn is a mess afterwards.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You hide things in your neightbourhood.";
        ev.redChoice.result_text = "You had fun. But the fun ends when it turns out your eight year old niece got lost. You spend the evening looking for her.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "You hide things all over your town.";
        ev.blueChoice.result_text = "The adults in your family have a blast, but the kids are bored to tears. You buy them some icecream to shut them up.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #7
        ev = new eventData();
        ev.Title = "Family book club";
        ev.Text = "Your mom organizes a family book club. You are tasked with deciding on the first book.";
        ev.Image = popularityImages[4];

        ev.greenChoice.choice_text = "Some Harry Potter should be fun.";
        ev.greenChoice.result_text = "The kids love the suggestion, but your dad doesnt like it.";
        ev.greenChoice.happiness = 0;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Some Stephen King will tickle.";
        ev.redChoice.result_text = "Nobody had fun reading IT after watching the movies. The kids find it too scary.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Some Edgar Allan Poe classics should be good.";
        ev.blueChoice.result_text = "Everyone reads a few short stories by Poe and he is a hit. The kids love the Raven while others liked the black cat.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #8
        ev = new eventData();
        ev.Title = "Chore night";
        ev.Text = "The house is a mess. Chore night!";
        ev.Image = popularityImages[1];

        ev.greenChoice.choice_text = "You make a game of it.";
        ev.greenChoice.result_text = "Nobody is fooled.";
        ev.greenChoice.happiness = 0;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "The kids will take care of it while you relax.";
        ev.redChoice.result_text = "The kids toss out some valuable action figures.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = 0;

        ev.blueChoice.choice_text = "The more that do chores the faster they go by.";
        ev.blueChoice.result_text = "Everyone does their bit and the house is clean in record time. You all go out for hotdogs.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #9
        ev = new eventData();
        ev.Title = "Nostalgia";
        ev.Text = "You show your younger family members things from your youth.";
        ev.Image = popularityImages[5];

        ev.greenChoice.choice_text = "You show them some old fashion clothes";
        ev.greenChoice.result_text = "'Haha, you really wore that?'";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You spin up some CD's.";
        ev.redChoice.result_text = "Turns out Hendrix is eternally hip.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You bring out some old magazines.";
        ev.blueChoice.result_text = "Your wife points out these could be worth some money. You do some research and find out they are quite valuable.";
        ev.blueChoice.happiness = 0;
        ev.blueChoice.money = 2;
        ev.blueChoice.popularity = 0;

        popularityEvents.Add(ev);

        // family #10
        ev = new eventData();
        ev.Title = "Jigsaw puzzle";
        ev.Text = "You bring out a jigsaw puzzle for everyone to enjoy.";
        ev.Image = popularityImages[4];

        ev.greenChoice.choice_text = "You bring out a bird puzzle.";
        ev.greenChoice.result_text = "Your youngest child chokes on a piece. The rest of the night is spent at the emergency room.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You bring out a comic puzzle.";
        ev.redChoice.result_text = "Your family makes fun of the characters. They were quite cool as you remembered them.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You bring out the worlds hardest puzzle.";
        ev.blueChoice.result_text = "Legends say that on col nights your grandma can still be heard in the distance cursing that puzzle.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #11
        ev = new eventData();
        ev.Title = "Family hike";
        ev.Text = "The family goes for a hike.";
        ev.Image = popularityImages[0];

        ev.greenChoice.choice_text = "You choose a hilly path.";
        ev.greenChoice.result_text = "The view from the hill is breathtaking. You have a good time.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "You choose a mountainous path.";
        ev.redChoice.result_text = "You run for your life when you meet a not so friendly mountain lion.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "You choose a flat path.";
        ev.blueChoice.result_text = "It was a nice walk, but rather dull.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #12
        ev = new eventData();
        ev.Title = "Mini golf";
        ev.Text = "You bring the family out for a round of mini golf.";
        ev.Image = popularityImages[3];

        ev.greenChoice.choice_text = "You try your hand at the windmill course.";
        ev.greenChoice.result_text = "You manage to win a small prize when you go hole in one on it.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 1;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "You go for Mt.Murderhorn.";
        ev.redChoice.result_text = "You lose five balls and one club before you admit defeat. You family finds this hilarious.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "You go for the clown course.";
        ev.blueChoice.result_text = "You didnt realize that the course came with an actual clown to harass you. You had fun tho.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #13
        ev = new eventData();
        ev.Title = "Zoo";
        ev.Text = "A nearby zoo has a family discount which you take advantage of.";
        ev.Image = popularityImages[0];

        ev.greenChoice.choice_text = "'Hey kids, look, monkeys!'";
        ev.greenChoice.result_text = "The car ride home was filled with interesting questions about monkeys and throwing things.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "'Hey kids, look, snakes!";
        ev.redChoice.result_text = "The car ride home was filled with questions about snakes and small rodents.";
        ev.redChoice.happiness = -1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "'Hey kids, look, a bear!'";
        ev.blueChoice.result_text = "The bear was pretty cool. It scared the kids tho.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #14
        ev = new eventData();
        ev.Title = "Story time";
        ev.Text = "The family settles down for a few stories from your older relatives.";
        ev.Image = popularityImages[4];

        ev.greenChoice.choice_text = "'Lets listen to grandpa!'";
        ev.greenChoice.result_text = "Turns out grandpa was a guard in WW2. At a camp. Lived in Brazil for a while.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "'Lets listen to grandma!'";
        ev.redChoice.result_text = "Grandma was a con-woman back in the eighties. Managed to steal quite a lot of money. Who knew?";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 1;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "'Lets listen to uncle Joe!'";
        ev.blueChoice.result_text = "Turns out Joe ran a pharmaceutical business out of the trunk of his car for a few years.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #15
        ev = new eventData();
        ev.Title = "Talent show";
        ev.Text = "Who knows, maybe someone in the family has hidden talent.";
        ev.Image = popularityImages[2];

        ev.greenChoice.choice_text = "Grandma's ping pong trick?";
        ev.greenChoice.result_text = "Grandma is handy with a spade. Can do all sorts of tricks. Was champion in her home town as a teenager.";
        ev.greenChoice.happiness = 1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 1;

        ev.redChoice.choice_text = "Aunt Julie and her muffins?";
        ev.redChoice.result_text = "Julie makes amazing muffins. You go into business together and make a small profit selling her special muffins to college kids.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = 1;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "Jenny's disappearance trick?";
        ev.blueChoice.result_text = "She can make the contents of a whole vodka bottle disappear in ten seconds. Truly amazing.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #16
        ev = new eventData();
        ev.Title = "Trivia";
        ev.Text = "Trivia night! Lets bring out those questions and rewards.";
        ev.Image = popularityImages[4];

        ev.greenChoice.choice_text = "Subject: world history.";
        ev.greenChoice.result_text = "Grandpa has impressive knowledge of everything german. Wait a sec!";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "Subject: Snowglobes around the world.";
        ev.redChoice.result_text = "Your wife has collected the darn things all of her life. She humiliates the rest of the family.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Obscure Lovecraft lore.";
        ev.blueChoice.result_text = "Everyone is having fun until the old one from beyond the void shows up to answer questions.";
        ev.blueChoice.happiness = -1;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = -1;

        popularityEvents.Add(ev);

        // family #17
        ev = new eventData();
        ev.Title = "Datenight";
        ev.Text = "You take your significant other out for a datenight, just the two of you.";
        ev.Image = popularityImages[2];

        ev.greenChoice.choice_text = "La'Amore, Ristorante.";
        ev.greenChoice.result_text = "The evening was great. You two fell in love again, and then had your first fight again when the bill came.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = -2;
        ev.greenChoice.popularity = -1;

        ev.redChoice.choice_text = "Cant go wrong with McDonalds.";
        ev.redChoice.result_text = "Your wife wore her best and assumed a classier place. She was not impressed.";
        ev.redChoice.happiness = 0;
        ev.redChoice.money = -1;
        ev.redChoice.popularity = -1;

        ev.blueChoice.choice_text = "Generic italian restaurant.";
        ev.blueChoice.result_text = "The evening was perfect apart from two dogs outside the restaurant that kept trying to eat the same piece of spaghetti.";
        ev.blueChoice.happiness = 1;
        ev.blueChoice.money = -1;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);

        // family #18
        ev = new eventData();
        ev.Title = "Arts and crafts";
        ev.Text = "You get your family to do some arts and crafts.";
        ev.Image = popularityImages[4];

        ev.greenChoice.choice_text = "Gather family photos for a photo album";
        ev.greenChoice.result_text = "Your dad accidentally gives you some candit photography. You'll never unsee that.";
        ev.greenChoice.happiness = -1;
        ev.greenChoice.money = 0;
        ev.greenChoice.popularity = 0;

        ev.redChoice.choice_text = "Paper Machee art contest.";
        ev.redChoice.result_text = "Everyone has fun and Bill, your sisters youngest, wins with his rendition of a snowman.";
        ev.redChoice.happiness = 1;
        ev.redChoice.money = 0;
        ev.redChoice.popularity = 1;

        ev.blueChoice.choice_text = "Scap booking";
        ev.blueChoice.result_text = "A nice evening is had by all. You will treasure your book for years to come.";
        ev.blueChoice.happiness = 2;
        ev.blueChoice.money = 0;
        ev.blueChoice.popularity = 1;

        popularityEvents.Add(ev);
    }
}

