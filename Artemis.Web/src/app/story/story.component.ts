import { Component, ElementRef, HostBinding, Renderer2 } from '@angular/core';
import { TavernService } from '../services/tavern.service';
import { Action, TavernModel, TavernModelOption } from '../services/models/tavern';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'artemis-story',
  templateUrl: './story.component.html',
  styleUrls: ['./story.component.scss']
})
export class StoryComponent {
  public openDoorResult?: TavernModel;
  @HostBinding('class.door-open')
  public doorIsOpen: boolean;
  public actions: Action[];
  @ViewChild("details")
  public details?: ElementRef;
  constructor(private tavernService: TavernService, private renderer: Renderer2) {
    this.doorIsOpen = false;
    this.actions = [];
  }

  openDoor() {
    this.doorIsOpen = true;
    this.tavernService.enterTavern().subscribe(result => {
      this.openDoorResult = result;
    });
  }

  selectOption(selectedOption: TavernModelOption)  {
    if (this.openDoorResult) {
        this.actions.push({
          description: this.openDoorResult.tavernModelDescription.description,
          action: selectedOption.description
        });
        this.tavernService.action({
          prompt: selectedOption.actionName,
          messages: this.openDoorResult.messages
        }).subscribe(result => {
          this.openDoorResult = result;debugger;
          if (this.details) {
            this.renderer.setAttribute(this.details.nativeElement, 'scrollTop', this.details.nativeElement.scrollHeight);
          }
        });
        this.openDoorResult = undefined;
    }
  }

  stringify(obj: any) {
    return JSON.stringify(obj, null, 4);
  }
}
