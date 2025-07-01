# test-project-1
![image](https://github.com/user-attachments/assets/d7eada79-9964-4442-a885-6624d0fcc871)

<div>
    <p><strong>Technologies:</strong> Unity UI, AI Navigation, MVC, State Machine</p>
</div>

<h4>Description:</h4>
<p>The project implements a resource collection system involving bots from two factions.</p>
<p>Random resources spawn at random positions in the environment. Each base has its own bots that automatically collect resources according to a predefined algorithm.</p>
<p>Navigation is handled by NavMesh agents, allowing bots to effectively avoid obstacles. Bot behavior is organized using the State Machine pattern.</p>
<p>The project uses the MVC architectural pattern. There is an entry point responsible for initializing core game entities. Object logic updates are centralized via a TickManager.</p>

<p>A simple user interface allows:</p>
<ul>
    <li><span>Adjusting the number of bots at each base</span></li>
    <li><span>Changing bot movement speed</span></li>
    <li><span>Configuring resource spawn rate</span></li>
    <li><span>Enabling or disabling path visualization</span></li>
    <li><span>Displaying current resource amounts at each base</span></li>
</ul>