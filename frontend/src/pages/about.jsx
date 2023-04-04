
const AboutPage = () => { 
    return <div className="about-page container-80">
        <h2 className="text-center page-title">CHÀO MỪNG ĐẾN VỚI THƯ VIỆN THẦY ÁI</h2>
        <h2 className="text-center mb-20">Giới thiệu</h2>
        <p className="intro mb-50"><i>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Porro vel perspiciatis ab optio doloremque quasi, numquam veritatis atque quisquam accusamus aperiam nulla tempore alias, tenetur ipsa voluptas eligendi ipsum. Dolores?
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Porro vel perspiciatis ab optio doloremque quasi, numquam veritatis atque quisquam accusamus aperiam nulla tempore alias, tenetur ipsa voluptas eligendi ipsum. Dolores?
        </i></p>
        <h2 className="text-center mb-20">Quy định</h2>
        <div className="rules">
            <div className="rule">
                <h3 className="mb-10">Quy định 1:</h3>
                <p>Độc giả chỉ được mượn cùng lúc tối đa 3 quyển sách. Nếu muốn mượn thêm phải trả sách trước</p>
            </div>
            <div className="rule">
                <h3 className="mb-10">Quy định 2:</h3>
                <p>Khi làm mất hoặc hỏng sách độc giả phải báo cáo trung thực và sớm nhất cho thủ thư tại thư viện</p>
            </div>
            <div className="rule">
                <h3 className="mb-10">Quy định 3:</h3>
                <p>Không cho phép mượn sách dùm. Trả dùm thì được</p>
            </div>
            <div className="rule">
                <h3 className="mb-10">Quy định 4:</h3>
                <p>Làm mất sách hoặc hỏng sách thì thẻ thư viện sẽ không có hiệu lực sử dụng (tạm thời là không sử dụng) và phải bồi thường để có hiệu lực sử dụng (sử dụng được). </p>
            </div>
        </div>
    </div>
}

export default AboutPage