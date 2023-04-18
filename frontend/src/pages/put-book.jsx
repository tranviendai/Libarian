import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom";
import CallApi, {CallApiWithToken} from "../utils/callApi";
import {convertToDMY, convertToYMD} from "../utils/convertDate";
import useGlobalContext from "../contexts/GlobalContext";

const PutBookPage = () => { 
    const { id } = useParams();
    const { token } = useGlobalContext();

    const DefaultFormData = { title: '', author: '', publisher: '', publishingYear: '2000-01-01', summary: '', categoryID: '-1' }
    const [form, setForm] = useState(DefaultFormData);
    const [categories, setCategories] = useState([]);
    const navigate = useNavigate();

    //category
    useEffect(() => { 
        let mounted = true;

        const fetchApi = async () => { 
            try {
                const resp = await CallApi.get('/categories');
                const data = resp.data;
                if (mounted) setCategories(data);
            } catch (err) { 
                console.log(err);
            }
        }

        fetchApi();

        return () => { 
            mounted = false;
        }
    }, [])

    //book
    useEffect(() => { 
        let mounted = true;

        const fetchApi = async () => { 
            try {
                const resp = CallApi.get('/books/' + id);
                const data = (await resp).data;
                const book = data;

                if(mounted) setForm({
                    title: book.title,
                    author: book.author,
                    publisher: book.publisher,
                    publishingYear: convertToYMD(book.publishingYear),
                    summary: book.summary,
                    categoryID: book.categoryID
                })
            } catch (err) { 
                console.log(err)
            }
        }

        if (id) fetchApi();

        return () => { 
            mounted = false;
        }
    }, [id])

    const onFormChange = (e) => { 
        const { id, value } = e.target;
        setForm(x => { return { ...x , [id]: value} })
    }

    const onSubmit = async () => { 
        try {

            if (form.categoryID < 0) { 
                alert('Vui lòng chọn thể loại');
                return;
            }
            const body = {
                ...form,
                publishingYear: convertToDMY(form.publishingYear),
                bookID: id || 'id',
                image: 'img'
            }
            if (id) {
                await CallApiWithToken(token).put('/books/' + id, body)
            } else { 
                await CallApiWithToken(token).post('/books/', body)
            }

            alert('Thành công!')
            if (id) navigate('/BLibrary/Book/' + id);
            else setForm(DefaultFormData);
        } catch (err) { 
            console.log(err);
            alert('Có lỗi!');
        }
    }

    return <div className="put-book-page">
        <div className="container-80">
            <div className="page-title">{id ? 'Sửa sách' :'Thêm Sách'}</div>
            <div className="flex-center">
                <form action="#">
                    <div>
                        <label htmlFor="title">Tên sách: </label>
                        <input type="text" id="title" onChange={onFormChange} value={form['title']} />
                    </div>
                    <div>
                        <label htmlFor="author">Tác giả</label>
                        <input type="text" id="author" onChange={onFormChange} value={form['author']} />
                    </div>
                    <div>
                        <label htmlFor="publisher">Nhà xuất bản</label>
                        <input type="text" id="publisher" onChange={onFormChange} value={form['publisher']} />
                    </div>
                    <div>
                        <label htmlFor="publishingYear">Năm xuất bản</label>
                        <input type="date" id="publishingYear" onChange={onFormChange} value={form['publishingYear']} />
                    </div>
                    <div>
                        <label htmlFor="summary">Tóm tắt</label>
                        <input type="text" id="summary" onChange={onFormChange} value={form['summary']} />
                    </div>
                    <div>
                        <label htmlFor="categoryID">Thể loại</label>
                        <select id="categoryID" onChange={onFormChange} value={form['categoryID']}>
                            <option key={-1} value={-1} hidden>Chọn thể loại</option>
                            {categories.map(x => <option key={x.categoryID} value={x.categoryID}>{x.nameCategory}</option>)}
                        </select>
                    </div>
                    <div className="btn btn-submit" onClick={onSubmit}>Xác nhận</div>
                </form>
            </div>
        </div>
    </div> 
}

export default PutBookPage