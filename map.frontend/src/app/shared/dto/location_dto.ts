export class location_dto {
    locationid: string = '';
    projectid: string = '';
    projectname: string = '';
    locationname: string = '';
    locationinfo: string = '';
    ward_code: string = '';
    district_code: string = '';
    province_code: string = '';
    address_detail: string = '';
    address: string = '';
    location: any;
    locationstatus: string = ''; //0: không trồng cây, 1: cây ổn định, 2: cây bị chết
    treecode: string = '';
    treename: string = '';
    treeinfor: string = '';
    treetype: string = '';
    treestatus: string = ''; //0: đã chết, 1: đang sống, 2: đã chuyển sang vị trí khác
    color: string = ''; //A: tất cả các điểm đồng màu, B: đánh dấu điểm cần khác màu
    record_stat: string = '';
    lstlocations: location_dto[] = [];

    location_lat: number = 0;
    location_lon: number = 0;

    lstUserid: string[] = [];
}