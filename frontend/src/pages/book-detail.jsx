import { useEffect, useMemo, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import CallApi, {CallApiWithToken} from '../utils/callApi';
import { getBookCover, booksData} from '../mock-data'
import FormEditCopy from "../components/page/book-detail/frm-edit-copy";
import useGlobalContext from "../contexts/GlobalContext";
import useFetch from "../utils/useFetch";
import DataTable from "../components/shared/data-table";

const Statuses = ["Còn sách", "Đang mượn", "Sách hỏng", "Sách mất"];

const BookDetailPage = () => {
    const {token} = useGlobalContext();

    const { id } = useParams();
    const [book, setBook] = useState(null);
    const [category, setCategory] = useState('');
    const [refeshCopies, setRefeshCopies] = useState(false);
    const [showEditDialog, setShowEditDialog] = useState(false);
    const [selectedCopy, setSelectedCopy] = useState(null);
    const [status, setStatus] = useState('');

    // eslint-disable-next-line
    const statusObj = useMemo(() => { return { params: { bookID: id, status: status||null } } }, [id, refeshCopies, status, token])
    const { data: copies } = useFetch('/lbooks/', statusObj, token);

    //copy table
    const headers = ['Mã cuốn sách', 'Tình trạng', 'Ghi chú'];
    const rows = copies?.map(x => {
        return {
            onRowSelected: () => selectCopy(x),
            rowData: ['#' + x.lBookID, x.status, x.note]
        }
    })

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
                //setCategory(CategoriesData.filter(x => x.categoryID === book.categoryID)[0].cate_name)
                console.log(err);
            }
        }

        fetchApi();

        return () => {
            mounted = false;
        }
    }, [book])

    const onDeleteBook = async () => { 
        try {
            if (!window.confirm("Xoa ha")) return;
            await CallApiWithToken(token).delete('books/' + id);
            alert('đã xóa')
            navigate('/BLibrary/Book')
        } catch (err) { 
            console.log(err);
            alert('có lỗi xảy ra');
            return false;
        }
    }

    const onAddCopy = async () => {
        try {
            await CallApiWithToken(token).post('lbooks/', {
                lBookID: 'lBookID',
                status: Statuses[0],
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
                            {token && <>
                                <div className="btn btn-primary" onClick={() => navigate(`/BLibrary/UpdateBook/${book.bookID}`)}>
                                    Chỉnh Sửa
                                </div>
                                <div className="btn btn-danger" onClick={onDeleteBook}>
                                    Xóa
                                </div>
                            </>}
                        </div>
                    </div>
                    <h3>Tóm tắt: </h3>
                    <div>{book.summary}</div>
                    <br />
                    
                    {token && <>
                        <hr />
                        <br />
                        <h2 className="page-title">Thông tin các cuốn sách</h2>
                        <div className="btn" onClick={onAddCopy}>+ Thêm cuốn sách</div>
                        
                        <div className="mt-3 mb-3">
                            <label className="me-3">Lọc theo tình trạng: </label>
                            <select value={status} onChange={e => setStatus(e.target.value)}>
                                <option value=''>Tất cả</option>
                                {Statuses.map(x => <option key={x} value={x}>{x}</option>)}
                            </select>
                        </div>
                        <div className="copy-list">

                            {(copies && copies.length > 0) ? <DataTable headers={headers} rows={rows} /> : 'Không tìm thấy'}
                        </div>
                    </>} 
                    
                </>
                :
                <h2>không tìm thấy sách</h2>
            }
        </div>
    </div>
}

export default BookDetailPage;