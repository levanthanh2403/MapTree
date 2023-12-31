import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

export interface NavigationItem {
  id: string;
  title: string;
  type: 'item' | 'collapse' | 'group';
  translate?: string;
  icon?: string;
  hidden?: boolean;
  url?: string;
  classes?: string;
  exactMatch?: boolean;
  external?: boolean;
  target?: boolean;
  breadcrumbs?: boolean;
  function?: any;
  badge?: {
    title?: string;
    type?: string;
  };
  children?: Navigation[];
}

export interface Navigation extends NavigationItem {
  children?: NavigationItem[];
}

const NavigationItems = [
  {
    id: 'navigation',
    title: 'Ứng dụng',
    type: 'group',
    icon: 'icon-navigation',
    children: [
      // {
      //   id: 'dashboard',
      //   title: 'Bản đồ khu vực',
      //   type: 'item',
      //   url: '/dashboard',
      //   icon: 'feather icon-home',
      //   classes: 'nav-item',
      // },
      {
        id: 'map',
        title: 'Bản đồ',
        type: 'item',
        url: '/home/map',
        icon: 'feather icon-pie-chart',
        classes: 'nav-item',
      },
      {
        id: 'news',
        title: 'Tin tức',
        type: 'item',
        url: '/home/news',
        icon: 'feather icon-home',
        classes: 'nav-item',
      },
      {
        id: 'report',
        title: 'Báo cáo',
        type: 'item',
        url: '/home/report',
        icon: 'feather icon-file-text',
        classes: 'nav-item',
      }
    ],
  },
  {
    id: 'system-config',
    title: 'Hệ thống',
    type: 'group',
    icon: 'icon-ui',
    children: [
      {
        id: 'basic',
        title: 'Thiết lập chung',
        type: 'collapse',
        icon: 'feather icon-box',
        children: [
          {
            id: 'projects',
            title: 'Dự án',
            type: 'item',
            url: '/system/projects',
          },
          {
            id: 'locations',
            title: 'Vị trí',
            type: 'item',
            url: '/system/locations',
          },
          {
            id: 'users',
            title: 'Tài khoản',
            type: 'item',
            url: '/system/users',
          }
        ],
      },
    ],
  },
  // {
  //   id: 'ui-element',
  //   title: 'UI ELEMENT',
  //   type: 'group',
  //   icon: 'icon-ui',
  //   children: [
  //     {
  //       id: 'basic',
  //       title: 'Component',
  //       type: 'collapse',
  //       icon: 'feather icon-box',
  //       children: [
  //         {
  //           id: 'button',
  //           title: 'Button',
  //           type: 'item',
  //           url: '/basic/button',
  //         },
  //         {
  //           id: 'badges',
  //           title: 'Badges',
  //           type: 'item',
  //           url: '/basic/badges',
  //         },
  //         {
  //           id: 'breadcrumb-pagination',
  //           title: 'Breadcrumb & Pagination',
  //           type: 'item',
  //           url: '/basic/breadcrumb-paging',
  //         },
  //         {
  //           id: 'collapse',
  //           title: 'Collapse',
  //           type: 'item',
  //           url: '/basic/collapse',
  //         },
  //         {
  //           id: 'tabs-pills',
  //           title: 'Tabs & Pills',
  //           type: 'item',
  //           url: '/basic/tabs-pills',
  //         },
  //         {
  //           id: 'typography',
  //           title: 'Typography',
  //           type: 'item',
  //           url: '/basic/typography',
  //         },
  //       ],
  //     },
  //   ],
  // },
  // {
  //   id: 'forms',
  //   title: 'Forms & Tables',
  //   type: 'group',
  //   icon: 'icon-group',
  //   children: [
  //     {
  //       id: 'forms-element',
  //       title: 'Form Elements',
  //       type: 'item',
  //       url: '/forms/basic',
  //       classes: 'nav-item',
  //       icon: 'feather icon-file-text',
  //     },
  //     {
  //       id: 'tables',
  //       title: 'Tables',
  //       type: 'item',
  //       url: '/tables/bootstrap',
  //       classes: 'nav-item',
  //       icon: 'feather icon-server',
  //     },
  //   ],
  // },
  // {
  //   id: 'chart-maps',
  //   title: 'Chart',
  //   type: 'group',
  //   icon: 'icon-charts',
  //   children: [
  //     {
  //       id: 'apexChart',
  //       title: 'ApexChart',
  //       type: 'item',
  //       url: 'apexchart',
  //       classes: 'nav-item',
  //       icon: 'feather icon-pie-chart',
  //     },
  //   ],
  // },
  {
    id: 'pages',
    title: 'Pages',
    type: 'group',
    icon: 'icon-pages',
    children: [
      // {
      //   id: 'auth',
      //   title: 'Authentication',
      //   type: 'collapse',
      //   icon: 'feather icon-lock',
      //   children: [
      //     {
      //       id: 'signup',
      //       title: 'Sign up',
      //       type: 'item',
      //       url: '/auth/signup',
      //       target: true,
      //       breadcrumbs: false,
      //     },
      //     {
      //       id: 'signin',
      //       title: 'Sign in',
      //       type: 'item',
      //       url: '/auth/signin',
      //       target: true,
      //       breadcrumbs: false,
      //     },
      //   ],
      // },
      // {
      //   id: 'sample-page',
      //   title: 'Sample Page',
      //   type: 'item',
      //   url: '/sample-page',
      //   classes: 'nav-item',
      //   icon: 'feather icon-sidebar',
      // },
      // {
      //   id: 'disabled-menu',
      //   title: 'Disabled Menu',
      //   type: 'item',
      //   url: 'javascript:',
      //   classes: 'nav-item disabled',
      //   icon: 'feather icon-power',
      //   external: true,
      // },
      {
        id: 'buy_now',
        title: 'Buy Now',
        type: 'item',
        icon: 'feather icon-book',
        classes: 'nav-item',
        url: 'https://codedthemes.com/item/datta-able-angular/',
        target: true,
        external: true,
      },
    ],
  },
];

@Injectable()
export class NavigationItem {
  role: string = '';
  constructor(private authService: AuthService) {
  }
  get() {
    var _data = [];
    var _tokenInfo = this.authService.getAuthFromLocalStorage();
    if (_tokenInfo != null) {
      this.role = _tokenInfo.role;
    }
    if(this.role == 'ADMIN') {
      _data = NavigationItems;
    } else if (this.role == 'ETP') {
      _data = NavigationItems.filter(o => o.id != 'system-config');
    } else if (this.role == 'USR') {
      _data = NavigationItems.filter(o => o.id != 'system-config');
    } else if (this.role == 'STAFF') {
      _data = NavigationItems.filter(o => o.id != 'system-config');
    }
    console.log('_data', _data);
    return _data;
  }
}
