import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import CallApi from '../utils/callApi';
import { getBookCover, booksData, BookCopyData, CategoriesData } from '../mock-data'
import FormEditCopy from "../components/page/book-detail/frm-edit-copy";

const BookDetailPage = () => {
    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [category, setCategory] = useState('');
    const [copies, setCopies] = useState([]);
    const [refeshCopies, setRefeshCopies] = useState(false);
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [selectedCopy, setSelectedCopy] = useState(null);

    //const CopyStates = ['Còn', 'Đang cho mượn', 'Bị hỏng', 'Bị mất'];
    const navigate = useNavigate();

    //book
    useEffect(() => {
        let mounted = true;

        const fetchApi = async () => {
            try {
                const resp = await CallApi.get('/books/' + id);
                const data = resp.data;
                
                if (mounted) setBook({ ...data, image: getBookCover(data.bookID, 200, 300) });
            } catch (err) {
                console.log(err);
                console.log(booksData.filter(x => x.book_id === id));
                if (mounted) setBook(booksData.filter(x => x.book_id === parseInt(id))[0]);
            }
        }

        fetchApi();

        return () => {
            mounted = false;
        }
    }, [id])

    //category
    useEffect(() => {
        let mounted = true;

        const fetchApi = async () => {
            try {
                const resp = await CallApi.get('/categories/' + book.categoryID);
                const data = resp.data;

                if (mounted) setCategory(data.nameCategory);
            } catch (err) {
                setCategory(CategoriesData.filter(x => x.categoryID === book.categoryID)[0].cate_name)
                console.log(err);
            }
        }

        fetchApi();

        return () => {
            mounted = false;
        }
    }, [book])

    //copies
    useEffect(() => {
        let mounted = true;

        const fetchApi = async () => {
            try {
                const resp = await CallApi.get('/lbooks/', {
                    params: {
                        bookID: id
                    }
                });
                const data = resp.data;

                if (mounted) setCopies(data);
            } catch (err) {
                console.log(err);
                setCopies(BookCopyData.filter(x => x.book_id === parseInt(id)))
            }
        }

        fetchApi();

        return () => {
            mounted = false;
        }
    }, [id, refeshCopies])

    const onDeleteBook = async () => { 
        try {
            await CallApi.delete('books/' + id);
            alert('đã xóa')
            navigate('/LMS/Book')
        } catch (err) { 
            console.log(err);
            alert('có lỗi xảy ra');
            return false;
        }
    }

    const onAddCopy = async () => {
        try {
            await CallApi.post('lbooks/', {
                lBookID: 'lBookID',
                status: 'Còn sách',
                note: 'Sách mới',
                bookID: book.bookID
            })

            alert('Đã thêm');
            setRefeshCopies(x => !x)
        } catch (err) { 
            console.log(err);
            alert('Có lỗi xảy ra');
        }
    }

    const selectCopy = (copy) => { 
        setSelectedCopy(copy);
        setShowEditDialog(true);
    }

    return <div className="book-detail-page">
        <h2 className="page-title">Thông tin sách</h2>
        {showEditDialog && <FormEditCopy onExit={() => setShowEditDialog(false)} copy={selectedCopy} refresh={setRefeshCopies} />}

        <div className="container-80">
            {book ?
                <>
                    <div className="book-info">
                        <div className="left">
                            <div className="book-cover">
                                <img src={book.image} alt="" />
                            </div>
                        </div>
                        <div className="right">
                            <div className="name">{book.title}</div>
                            <div>Thể loại: {category}</div>
                            <div>Tác giả: {book.author}</div>
                            <div>NXB: {book.publisher}</div>
                            <div>Năm XB: {book.publishingYear}</div>
                            <div>Ngày thêm: {book.addDate}</div>
                            <div className="btn">
                                <Link to={`/LMS/UpdateBook/${book.bookID}`}>Chỉnh Sửa</Link>
                            </div>
                            <div className="btn" onClick={onDeleteBook}>
                                Xóa
                            </div>
                        </div>
                    </div>
                    <h3>Tóm tắt: </h3>
                    <div>{book.summary}</div>
                    <br />
                    <hr />
                    <br />
                    <h2 className="page-title">Thông tin các cuốn sách</h2>
                    <div className="btn" onClick={onAddCopy}>+ Thêm cuốn sách</div>
                    {copies.length > 0 &&
                        <>
                        <p><i>click vào cuốn sách để chỉnh sửa</i></p>
                        <div className="copy-list">
                            <div className="copy-item">
                                <div>Mã cuốn sách</div>
                                <div>Tình trạng</div>
                                <div>Ghi chú</div>
                            </div>
                            {copies.map(x => <div className="copy-item" key={x.lBookID} onClick={()=>selectCopy(x)}>
                                <div >{x.lBookID}</div>
                                <div >{x.status}</div>
                                <div >{x.note}</div>
                            </div>)}
                        </div>
                    </>
                    }
                </>
                :
                <h2>không tìm thấy sách</h2>
            }
        </div>
    </div>
}

export default BookDetailPage;