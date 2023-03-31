
const booksData = [
    {
        book_id: 1, coverImg: require('./resources/imgs/img_1.png'),
        book_name: 'Trong đáy mắt trời xanh là vĩnh viễn viễn viễn',
        author: 'Xuân Quỳnh',
        cate_id: 3,
        summary: 'Có rất nhiều biến thể của Lorem Ipsum mà bạn có thể tìm thấy, nhưng đa số được biến đổi bằng cách thêm các yếu tố hài hước, các từ ngẫu nhiên có khi không có vẻ gì là có ý nghĩa. Nếu bạn định sử dụng một đoạn Lorem Ipsum, bạn nên kiểm tra kĩ để chắn chắn là không có gì nhạy cảm được giấu ở giữa đoạn văn bản. Tất cả các công cụ sản xuất văn bản mẫu Lorem Ipsum đều được làm theo cách lặp đi lặp lại các đoạn chữ cho tới đủ thì thôi, khiến cho lipsum.com trở thành công cụ sản xuất Lorem Ipsum đáng giá nhất trên mạng. Trang web này sử dụng hơn 200 từ la-tinh, kết hợp thuần thục nhiều cấu trúc câu để tạo ra văn bản Lorem Ipsum trông có vẻ thật sự hợp lí. Nhờ thế, văn bản Lorem Ipsum được tạo ra mà không cần một sự lặp lại nào, cũng không cần chèn thêm các từ ngữ hóm hỉnh hay thiếu trật tự.' 
    },
    {
        book_id: 2, coverImg: require('./resources/imgs/img_2.png'), book_name: 'Những tấm lòng cao cả', author: 'Edmondo De Amicis', cate_id: 3 },
    {
        book_id: 3, coverImg: require('./resources/imgs/img_3.png'), book_name: 'Đừng lựa chọn an nhàn khi còn trẻ', author: 'Cảnh Thiên', cate_id: 3 },
    {
        book_id: 4, coverImg: require('./resources/imgs/img_4.png'), book_name: 'Tuổi trẻ đáng giá bao nhiêu', author: 'Rosie Nguyễn', cate_id: 1 },
    {
        book_id: 5, coverImg: require('./resources/imgs/img_5.png'), book_name: 'Kiếp nào ta cùng tìm thấy nhau', author: 'Brian L. Weiss', cate_id: 1 },
    {
        book_id: 6, coverImg: require('./resources/imgs/img_6.png'), book_name: 'Mật mã thần số học', author: 'Glynis Mccants', cate_id: 2 }
]

const CategoriesData = [
    { cate_id: 1, cate_name: 'Truyền cảm hứng' },
    { cate_id: 2, cate_name: 'Giải trí' },
    { cate_id: 3, cate_name: 'Lịch sử' },
    { cate_id: 4, cate_name: 'Giáo trình' },
    { cate_id: 5, cate_name: 'Truyện ngắn' },
    { cate_id: 6, cate_name: 'Tiểu sử' },
    { cate_id: 7, cate_name: 'Giả tưởng' },
    { cate_id: 8, cate_name: 'Tình cảm' },
    { cate_id: 9, cate_name: 'Văn học' },
    { cate_id: 10, cate_name: 'Kinh dị' }
]

const BookCopyData = [
    { copy_id: 'cp001', book_id: 1, state_id: 0, note: 'noted' },
    { copy_id: 'cp002', book_id: 2, state_id: 0, note: 'noted' },
    { copy_id: 'cp003', book_id: 3, state_id: 0, note: 'noted' },
    { copy_id: 'cp004', book_id: 4, state_id: 0, note: 'noted' },
    { copy_id: 'cp005', book_id: 5, state_id: 0, note: 'noted' },
    { copy_id: 'cp006', book_id: 6, state_id: 0, note: 'noted' },

    { copy_id: 'cp007', book_id: 1, state_id: 1, note: '' },
    { copy_id: 'cp008', book_id: 2, state_id: 1, note: '' },
    { copy_id: 'cp009', book_id: 3, state_id: 1, note: '' },
    { copy_id: 'cp010', book_id: 4, state_id: 1, note: '' },
    { copy_id: 'cp011', book_id: 5, state_id: 1, note: '' },
    { copy_id: 'cp012', book_id: 6, state_id: 1, note: '' },

    { copy_id: 'cp013', book_id: 2, state_id: 2, note: '' },
    { copy_id: 'cp014', book_id: 4, state_id: 2, note: '' },
    { copy_id: 'cp015', book_id: 6, state_id: 2, note: '' },

    { copy_id: 'cp016', book_id: 2, state_id: 3, note: '' },
    { copy_id: 'cp017', book_id: 3, state_id: 3, note: '' },
    { copy_id: 'cp018', book_id: 5, state_id: 3, note: '' }
]

const getBookCover = (img, w=123, h=184) => { 
    return `https://picsum.photos/seed/${img}/${w}/${h}`
}

export { booksData, CategoriesData, BookCopyData ,getBookCover }