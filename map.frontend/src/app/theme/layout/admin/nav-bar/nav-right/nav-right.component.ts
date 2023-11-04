import { Component } from '@angular/core';
import { NgbDropdownConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { animate, style, transition, trigger } from '@angular/animations';
import { AuthService } from 'src/app/services/auth.service';
import { ImageBase64 } from 'src/app/shared/ImageBase64';

@Component({
  selector: 'app-nav-right',
  templateUrl: './nav-right.component.html',
  styleUrls: ['./nav-right.component.scss'],
  providers: [NgbDropdownConfig],
  animations: [
    trigger('slideInOutLeft', [
      transition(':enter', [
        style({ transform: 'translateX(100%)' }),
        animate('300ms ease-in', style({ transform: 'translateX(0%)' })),
      ]),
      transition(':leave', [
        animate('300ms ease-in', style({ transform: 'translateX(100%)' })),
      ]),
    ]),
    trigger('slideInOutRight', [
      transition(':enter', [
        style({ transform: 'translateX(-100%)' }),
        animate('300ms ease-in', style({ transform: 'translateX(0%)' })),
      ]),
      transition(':leave', [
        animate('300ms ease-in', style({ transform: 'translateX(-100%)' })),
      ]),
    ]),
  ],
})
export class NavRightComponent {
  visibleUserList: boolean;
  chatMessage: boolean;
  friendId: boolean;
  userId: string = '';
  imgSrc: string = '';

  constructor(
    config: NgbDropdownConfig, 
    private authService: AuthService,
    private modalService: NgbModal) {
    config.placement = 'bottom-right';
    this.visibleUserList = false;
    this.chatMessage = false;
    var _tokenInfo = authService.getAuthFromLocalStorage();
    this.userId = _tokenInfo.username;
    this.imgSrc = (authService.getUserImage() == null || authService.getUserImage().img == '') ? ImageBase64.defaultImg : authService.getUserImage();
  }

  onChatToggle(friend_id) {
    this.friendId = friend_id;
    this.chatMessage = !this.chatMessage;
  }
  Logout() {
    this.authService.logout();
  }
  openModal(modal: any) {
    this.modalService.open(modal, { size: 'sm' });
  }

}
