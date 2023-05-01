import { booksData, CategoriesData, getBookCover } from '../mock-data';
import BooksGrid from '../components/shared/books-grid';
import { useEffect, useMemo, useState } from 'react';
import CallApi from '../utils/callApi'
import { Link, useLocation } from 'react-router-dom';
import useGlobalContext from '../contexts/GlobalContext';

const BookPage = () => { 

    const { state } = useLocation();
    const { search, selectCate } = state || {};
    const { token } = useGlobalContext();

    //STATES
    const [list, setList] = useState([]); //book list
    const [loading, setLoading] = useState(false); //book loading
    const [searchName, setSearchName] = useState(search || ''); //input controll
    const [keyword, setKeyword] = useState('' || search);  //input result -> keyword for query
    const [hint, setHint] = useState([]); //hint list
    const [showHint, setShowHint] = useState(false);
    const [searching, setSearching] = useState(false); //hint loading

    const [categories, setCategories] = useState([]);
    const [selectedCate, setSelectedCate] = useState(selectCate || -1);
    const [page, setPage] = useState(1);
    const [bookCount, setBookCount] = useState(0);
    const [orderBy, setOrderBy] = useState('addDate');
    const [orderAsc, setOrderAsc] = useState(false);

    const ItemPerPage = 20;

    //EFFECTS
    //book-list
    useEffect(() => { 
        let mounted = true;

        const fetchApi = async () => { 
            setLoading(true);
            try {
                const resp = await CallApi.get('/books', {
                    params: {
                        cateId: selectedCate > 0 ? selectedCate : null,
                        keyword,
                        limit: ItemPerPage,
                        page,
                        orderBy,
                        asc: orderAsc
                    }
                })

                const data = resp.data;
                if (mounted) setList(data.map(x => { return { ...x, image: getBookCover(x.bookID) } }));
            } catch (err) {
                console.log(err);
                let data = booksData.filter(x => x.book_name.toLowerCase().startsWith(keyword.toLowerCase()) && (selectedCate < 0 || x.cate_id+'' === selectedCate));
                setList(data);
            }
            finally { 
                setLoading(false);
            }
        }
        fetchApi();

        return () => { 
            mounted = false;
        }
    }, [keyword, selectedCate, page, orderAsc, orderBy])

    //search hint
    useEffect(() => { 
        let mounted = true;
        let timer;

        const fetchApi = async () => { 
            if (!searchName) return;

            setSearching(true);
            try {
                const resp = await CallApi.get('/books/utils/getsearchHint', {
                    params: {
                        keyword: searchName,
                        cateId: selectedCate > 0? selectedCate: null
                    }
                })

                const data = await resp.data;
                if (mounted) setHint(data);
            } catch (err) {
                console.log(err);
                let hints = booksData.filter(x => x.book_name.toLowerCase().startsWith(searchName.toLowerCase()) && (selectedCate < 0 || x.cate_id + '' === selectedCate))
                hints = hints.map(x => x.book_name);
                if (mounted) setHint(hints);
            } finally { 
                setSearching(false);
            }
        }

        if (searchName.length > 0) {
            timer = setTimeout(fetchApi, 300);
        } else { 
            setHint([]);
        }

        return () => { 
            mounted = false;
            if (timer) clearTimeout(timer);
        }
    }, [searchName, selectedCate])

    //category
    useEffect(() => { 
        let mounted = true;

        const fetchApi = async () => { 
            try {
                const resp = await CallApi.get('/categories')
                const data = (await resp).data;
                if (mounted) setCategories(data);
            } catch (err) {
                console.log(err);
                if (mounted) setCategories(CategoriesData);
            }
        }

        fetchApi();

        return () => { 
            mounted = false;
        }
    }, [])

    //count book
    useEffect(() => { 
        let mounted = true;

        const fetchApi = async () => { 
            try {
                const resp = await CallApi.get('/books/utils/countbook', {
                    params: {
                        keyword,
                        cateId: selectedCate > 0 ? selectedCate: null
                    }
                })
                const data = resp.data;
                if (mounted) setBookCount(data);
            } catch (err) { 
                console.log(err);
                if (mounted) setBookCount(booksData.length);
            }
        }

        fetchApi();

        return () => { 
            mounted = false
        }
    }, [keyword, selectedCate])

    const getTotalPage = useMemo(() => { 
        return Math.max(Math.ceil(bookCount / ItemPerPage), 1);
    }, [bookCount])
    
    const onSearch = () => {
        setKeyword(searchName);
        setPage(1);
    }

    const onChangeCategory = (e) => {
        setSelectedCate(e.target.value);
        setPage(1);
    }

    const onSearchKeyDown = (e) => { 
        if (e.key === 'Enter') onSearch();
    }

    const onOrderChange = (e) => { 
        setOrderAsc(e.target.id === 'asc')
    }

    return <div className="book-page">
        <h3 className="page-title">Tìm kiếm sách</h3>
        <div className="container-80">
            {token &&
                <div className="add-book-btn btn">
                    <Link to='/BLibrary/AddBook'>+ Thêm Sách</Link>
                </div>
            }
        </div>
        <div className="tool-bar container-80">
            <div>
                <label htmlFor='searchName'> Tựa sách: </label>
                <div className="input-wrap">
                    <input type='text' id='searchName' placeholder='Nhập tựa sách' value={searchName}
                        onChange={(e) => { setSearchName(e.target.value) }} onKeyDown={(e) => onSearchKeyDown(e)}
                        onFocus={() => setShowHint(true)} onBlur={() => setTimeout(() => setShowHint(false), 500)} />
                    
                    
                    {searchName.length > 0 &&
                        <div className={'hints ' + (showHint?'':'hide')}>
                            {searching ? <div><i>Đang tìm...</i></div> :
                                hint.length > 0 &&
                                hint.map(x => <div className="hint" key={x} onClick={() => { setSearchName(x);}}>{x}</div>)
                            }
                        </div>
                    } 
                </div>
                <div className="btn" onClick={onSearch}>search</div>
            </div>
            <div>
                <label htmlFor='category'>
                    Thể loại sách:
                </label>
                <select onChange={e => onChangeCategory(e)} value={selectedCate} id='category'>
                    <option value={-1} key={-1}>Tất cả</option>
                    {categories.map(x => <option value={x.categoryID} key={x.categoryID}>{x.nameCategory}</option>)}
                </select>
            </div>

            <div>
                <label>
                    Sắp xếp theo
                </label>
                <select onChange={(e) => setOrderBy(e.target.value)} value={orderBy}>
                    <option value="addDate">Ngày thêm vào</option>
                    <option value="title">Tên sách</option>
                </select>
                <label htmlFor='asc'>Tăng dần</label>
                <input type='radio' id='asc' name='orderby' checked={orderAsc} onChange={onOrderChange} />
                <label htmlFor='desc'>Giảm dần</label>
                <input type='radio' id='desc' name='orderby' checked={!orderAsc} onChange={onOrderChange}/>
            </div>
        </div>
        {loading && <div className="loader"></div>} 
        <div className="container-80">
            <BooksGrid bookList={list} nCol={5} />
            <div className="pager">
                Trang <input type="number" max={getTotalPage} min={1} value={page} onChange={(e) => setPage(e.currentTarget.value)} /> / {getTotalPage}
            </div>
        </div>
    </div>
}

export default BookPage