import { useState, useEffect } from "react";
import DialogWrapper from "./dialog-wrapper";
import CallApi from "../../utils/callApi";
import CateIcon from '../../resources/imgs/book_cate_icon.png'
import AuthorIcon from '../../resources/imgs/book_author_icon.png'
import PubIcon from '../../resources/imgs/book_publisher_icon.png'
import YearIcon from '../../resources/imgs/book_year_icon.png'
import DateIcon from '../../resources/imgs/book_adddate_icon.png'

const BookDetailDialog = ({ book, onExit }) => { 
    const [category, setCategory] = useState('');
    
    useEffect(() => {
        let mounted = true;

        const fetchApi = async () => {
            try {
                const resp = await CallApi.get('/categories/' + book.categoryID);
                const data = resp.data;

                if (mounted) setCategory(data.nameCategory);
            } catch (err) {
                //setCategory(CategoriesData.filter(x => x.categoryID === book.categoryID)[0].cate_name)
                console.log(err);
            }
        }

        fetchApi();

        return () => {
            mounted = false;
        }
    }, [book])

    return <DialogWrapper onClickOut={onExit}>
        <div className="book-detail-dialog">
            <div className="header">Thông Tin Sách</div>
            <h2>{book.title}</h2>
            <div className="desc">{book.summary}</div>
            <div className="info">
                <div className="left">
                    <p>
                        <img src={CateIcon} alt="" />
                        Thể Loại: {category}
                    </p>
                    <p>
                        <img src={AuthorIcon} alt="" />
                        Tác Giả: {book.author}
                    </p>
                    <p>
                        <img src={PubIcon} alt="" />
                        Nhà Xuất Bản: {book.publisher}
                    </p>
                    <p>
                        <img src={YearIcon} alt="" />
                        Năm Xuất Bản: {book.publishingYear}
                    </p>
                    <p>
                        <img src={DateIcon} alt="" />
                        Ngày Thêm: {book.addDate}
                    </p>
                </div>
                <div className="right">
                    <img src={book.image} alt="" />
                </div>
            </div>
            <div className="exit" onClick={onExit}>x</div>
        </div>
    </DialogWrapper>
}

export default BookDetailDialog;