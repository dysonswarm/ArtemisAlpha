import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'artemis-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  links = [
    { title: 'Story', fragment: '' },
    { title: 'History', fragment: 'history' }
  ];
  
  constructor(public route: ActivatedRoute) {}
}
