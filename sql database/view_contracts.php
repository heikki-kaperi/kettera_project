<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Contract Viewer</title>
    <style>
        body {
            font-family: 'Georgia', 'Times New Roman', serif;
            margin: 20px;
            background-color: #e0e0e0;
        }
        .container {
            max-width: 1000px;
            margin: 0 auto;
        }
        h1 {
            text-align: center;
            color: #333;
            margin-bottom: 30px;
        }
        .contract-selector {
            background-color: white;
            padding: 20px;
            border-radius: 8px;
            margin-bottom: 30px;
            box-shadow: 0 2px 8px rgba(0,0,0,0.1);
        }
        .contract-selector select {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            border: 2px solid #4CAF50;
            border-radius: 4px;
            margin-bottom: 10px;
        }
        .contract-selector button {
            width: 100%;
            padding: 12px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 4px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        .contract-selector button:hover {
            background-color: #45a049;
        }
        .print-button {
            background-color: #2196F3 !important;
            margin-bottom: 10px;
        }
        .print-button:hover {
            background-color: #0b7dda !important;
        }
        
        /* A4 Paper styling */
        .a4-paper {
            width: 210mm;
            min-height: 297mm;
            padding: 20mm;
            margin: 20px auto;
            background-color: white;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            page-break-after: always;
        }
        
        /* Contract header */
        .contract-header {
            text-align: center;
            border-bottom: 3px solid #333;
            padding-bottom: 20px;
            margin-bottom: 30px;
        }
        .contract-header h2 {
            margin: 0;
            color: #333;
            font-size: 28px;
            text-transform: uppercase;
            letter-spacing: 2px;
        }
        .contract-header .contract-number {
            color: #666;
            font-size: 14px;
            margin-top: 5px;
        }
        
        /* Contract metadata */
        .contract-metadata {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 15px;
            margin-bottom: 30px;
            padding: 15px;
            background-color: #f9f9f9;
            border-left: 4px solid #4CAF50;
        }
        .metadata-item {
            font-size: 14px;
        }
        .metadata-label {
            font-weight: bold;
            color: #555;
            margin-right: 5px;
        }
        .metadata-value {
            color: #333;
        }
        
        /* Status badges */
        .status-badge {
            display: inline-block;
            padding: 4px 12px;
            border-radius: 12px;
            font-size: 12px;
            font-weight: bold;
            margin-left: 10px;
        }
        .status-approved {
            background-color: #4CAF50;
            color: white;
        }
        .status-pending {
            background-color: #FF9800;
            color: white;
        }
        .status-sent {
            background-color: #2196F3;
            color: white;
        }
        
        /* Contract blocks */
        .contract-blocks {
            margin-top: 30px;
        }
        .contract-block {
            margin-bottom: 25px;
            page-break-inside: avoid;
        }
        .block-number {
            display: inline-block;
            background-color: #333;
            color: white;
            padding: 5px 15px;
            border-radius: 3px;
            font-size: 12px;
            font-weight: bold;
            margin-bottom: 10px;
        }
        .block-content {
            line-height: 1.8;
            text-align: justify;
            font-size: 12pt;
            color: #333;
            padding: 15px;
            background-color: #fafafa;
            border-left: 3px solid #4CAF50;
        }
        .block-modified {
            color: #FF9800;
            font-weight: bold;
        }
        .block-image {
            text-align: center;
            margin: 20px 0;
            page-break-inside: avoid;
        }
        .block-image img {
            max-width: 100%;
            max-height: 600px;
            border: 1px solid #ddd;
            padding: 5px;
            background-color: white;
        }
        
        /* Stakeholders section */
        .stakeholders-section {
            margin-top: 40px;
            page-break-inside: avoid;
        }
        .stakeholders-section h3 {
            color: #333;
            border-bottom: 2px solid #4CAF50;
            padding-bottom: 10px;
        }
        .stakeholder-list {
            margin-top: 15px;
        }
        .stakeholder-item {
            padding: 10px;
            margin-bottom: 10px;
            background-color: #f9f9f9;
            border-left: 4px solid #2196F3;
        }
        .stakeholder-approved {
            border-left-color: #4CAF50;
        }
        
        /* Comments section */
        .comments-section {
            margin-top: 40px;
            page-break-before: always;
        }
        .comments-section h3 {
            color: #333;
            border-bottom: 2px solid #4CAF50;
            padding-bottom: 10px;
        }
        .comment {
            padding: 15px;
            margin-bottom: 15px;
            background-color: #f9f9f9;
            border-left: 4px solid #9E9E9E;
            page-break-inside: avoid;
        }
        .comment-internal {
            border-left-color: #2196F3;
        }
        .comment-external {
            border-left-color: #FF9800;
        }
        .comment-header {
            font-weight: bold;
            margin-bottom: 5px;
            color: #555;
        }
        .comment-date {
            font-size: 11px;
            color: #888;
            margin-bottom: 10px;
        }
        
        /* Signature section */
        .signature-section {
            margin-top: 60px;
            page-break-inside: avoid;
        }
        .signature-line {
            margin-top: 80px;
            border-top: 2px solid #333;
            padding-top: 10px;
            width: 300px;
        }
        
        /* Print styles */
        @media print {
            body {
                background-color: white;
                margin: 0;
            }
            .container {
                max-width: 100%;
            }
            .contract-selector {
                display: none;
            }
            .a4-paper {
                width: 210mm;
                min-height: 297mm;
                padding: 20mm;
                margin: 0;
                box-shadow: none;
            }
            .print-button {
                display: none;
            }
        }
        
        .no-contract {
            text-align: center;
            padding: 40px;
            color: #666;
            font-size: 18px;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>📄 Contract Viewer</h1>
        
        <?php
        // Database connection
        $servername = "127.0.0.1";
        $username = "root";
        $password = "";
        $dbname = "kettera";

        $conn = new mysqli($servername, $username, $password, $dbname);

        if ($conn->connect_error) {
            die("Connection failed: " . $conn->connect_error);
        }

        $conn->set_charset("utf8mb4");

        // Get selected contract ID
        $selected_contract_id = isset($_GET['contract_id']) ? intval($_GET['contract_id']) : 0;

        // Get all contracts for dropdown
        $contracts_sql = "SELECT c.Contract_NR, c.Company_name, c.Created_date, c.Approved 
                          FROM contract c 
                          ORDER BY c.Contract_NR DESC";
        $contracts_result = $conn->query($contracts_sql);
        ?>

        <!-- Contract Selector -->
        <div class="contract-selector">
            <form method="GET" action="">
                <label for="contract_id"><strong>Select Contract:</strong></label>
                <select name="contract_id" id="contract_id" required>
                    <option value="">-- Choose a contract --</option>
                    <?php
                    if ($contracts_result->num_rows > 0) {
                        while($contract_row = $contracts_result->fetch_assoc()) {
                            $selected = ($contract_row['Contract_NR'] == $selected_contract_id) ? 'selected' : '';
                            $approved_text = $contract_row['Approved'] ? '✓' : '○';
                            echo '<option value="' . $contract_row['Contract_NR'] . '" ' . $selected . '>';
                            echo $approved_text . ' Contract #' . $contract_row['Contract_NR'] . ' - ' . 
                                 htmlspecialchars($contract_row['Company_name']) . ' (' . 
                                 date('d.m.Y', strtotime($contract_row['Created_date'])) . ')';
                            echo '</option>';
                        }
                    }
                    ?>
                </select>
                <button type="submit">View Contract</button>
                <?php if ($selected_contract_id > 0): ?>
                <button type="button" class="print-button" onclick="window.print()">🖨️ Print Contract</button>
                <?php endif; ?>
            </form>
        </div>

        <?php
        if ($selected_contract_id > 0) {
            // Get contract details
            $contract_sql = "SELECT c.*, 
                                    iu.First_name as Creator_First, 
                                    iu.Last_name as Creator_Last
                             FROM contract c
                             LEFT JOIN internal_user iu ON c.The_Creator = iu.Int_User_ID
                             WHERE c.Contract_NR = ?";
            
            $stmt = $conn->prepare($contract_sql);
            $stmt->bind_param("i", $selected_contract_id);
            $stmt->execute();
            $contract_result = $stmt->get_result();

            if ($contract_result->num_rows > 0) {
                $contract = $contract_result->fetch_assoc();
                ?>

                <!-- A4 Paper -->
                <div class="a4-paper">
                    <!-- Contract Header -->
                    <div class="contract-header">
                        <h2>Contract Agreement</h2>
                        <div class="contract-number">Contract No. <?php echo $contract['Contract_NR']; ?></div>
                    </div>

                    <!-- Contract Metadata -->
                    <div class="contract-metadata">
                        <div class="metadata-item">
                            <span class="metadata-label">Company:</span>
                            <span class="metadata-value"><?php echo htmlspecialchars($contract['Company_name']); ?></span>
                        </div>
                        <div class="metadata-item">
                            <span class="metadata-label">Created By:</span>
                            <span class="metadata-value">
                                <?php echo htmlspecialchars($contract['Creator_First'] . ' ' . $contract['Creator_Last']); ?>
                            </span>
                        </div>
                        <div class="metadata-item">
                            <span class="metadata-label">Created Date:</span>
                            <span class="metadata-value">
                                <?php echo date('d.m.Y H:i', strtotime($contract['Created_date'])); ?>
                            </span>
                        </div>
                        <div class="metadata-item">
                            <span class="metadata-label">Status:</span>
                            <span class="metadata-value">
                                <?php if ($contract['Approved']): ?>
                                    <span class="status-badge status-approved">✓ Approved</span>
                                <?php else: ?>
                                    <span class="status-badge status-pending">○ Pending</span>
                                <?php endif; ?>
                                
                                <?php if ($contract['Sent_to_external']): ?>
                                    <span class="status-badge status-sent">📤 Sent to External</span>
                                <?php endif; ?>
                            </span>
                        </div>
                    </div>

                    <!-- Contract Blocks -->
                    <div class="contract-blocks">
                        <h3 style="color: #333; border-bottom: 2px solid #4CAF50; padding-bottom: 10px;">Contract Terms</h3>
                        
                        <?php
                        // Get contract blocks
                        $blocks_sql = "SELECT cb.*, 
                                              cbs.Block_order,
                                              ocb.Category_name
                                       FROM contract_block cb
                                       INNER JOIN contract_blocks cbs ON cb.Contract_Block_NR = cbs.Contract_Block_NR
                                       LEFT JOIN original_contract_block ocb ON cb.Org_Cont_ID = ocb.Org_Cont_ID
                                       WHERE cbs.Contract_NR = ?
                                       ORDER BY cbs.Block_order";
                        
                        $blocks_stmt = $conn->prepare($blocks_sql);
                        $blocks_stmt->bind_param("i", $selected_contract_id);
                        $blocks_stmt->execute();
                        $blocks_result = $blocks_stmt->get_result();

                        if ($blocks_result->num_rows > 0) {
                            $block_count = 1;
                            while($block = $blocks_result->fetch_assoc()) {
                                echo '<div class="contract-block">';
                                
                                // Block number with category
                                echo '<div class="block-number">';
                                echo 'Section ' . $block_count;
                                if (!empty($block['Category_name'])) {
                                    echo ' - ' . htmlspecialchars($block['Category_name']);
                                }
                                if ($block['New']) {
                                    echo ' <span class="block-modified">[Modified]</span>';
                                }
                                echo '</div>';
                                
                            // Block content based on type
                            if ($block['Type'] == 1) {
                                // Image block
                                if (!empty($block['MediaContent']) && $block['MediaContent'] !== '0') {
                                    echo '<div class="block-image">';
                                    $imageData = base64_encode($block['MediaContent']);
                                    echo '<img src="data:image/jpeg;base64,' . $imageData . '" alt="Contract Image">';
                                    echo '</div>';
                                }
                                
                                // Show text if any
                                if (!empty($block['Contract_text'])) {
                                    echo '<div class="block-content">';
                                    echo nl2br(htmlspecialchars($block['Contract_text']));
                                    echo '</div>';
                                }
                            
                                } else {
                                    // Text block
                                    echo '<div class="block-content">';
                                    if (!empty($block['Contract_text'])) {
                                        echo nl2br(htmlspecialchars($block['Contract_text']));
                                    } else {
                                        echo '<em>(No content)</em>';
                                    }
                                    echo '</div>';
                                }
                                
                                echo '</div>';
                                $block_count++;
                            }
                        } else {
                            echo '<p class="no-contract">No contract blocks found for this contract.</p>';
                        }
                        ?>
                    </div>

                    <?php
                    // Get stakeholders
                    $stakeholders_sql = "SELECT cs.*, 
                                                iu.First_name, 
                                                iu.Last_name,
                                                iu.Email
                                         FROM contract_stakeholders cs
                                         INNER JOIN internal_user iu ON cs.Int_User_ID = iu.Int_User_ID
                                         WHERE cs.Contract_NR = ?";
                    
                    $stake_stmt = $conn->prepare($stakeholders_sql);
                    $stake_stmt->bind_param("i", $selected_contract_id);
                    $stake_stmt->execute();
                    $stakeholders_result = $stake_stmt->get_result();

                    if ($stakeholders_result->num_rows > 0):
                    ?>
                    <!-- Stakeholders Section -->
                    <div class="stakeholders-section">
                        <h3>Internal Stakeholders & Approvals</h3>
                        <div class="stakeholder-list">
                            <?php
                            while($stakeholder = $stakeholders_result->fetch_assoc()) {
                                $approved_class = $stakeholder['Approved'] ? 'stakeholder-approved' : '';
                                echo '<div class="stakeholder-item ' . $approved_class . '">';
                                echo '<strong>' . htmlspecialchars($stakeholder['First_name'] . ' ' . $stakeholder['Last_name']) . '</strong><br>';
                                echo 'Email: ' . htmlspecialchars($stakeholder['Email']) . '<br>';
                                echo 'Approval Rights: ' . ($stakeholder['Has_approval_rights'] ? 'Yes' : 'No') . '<br>';
                                
                                if ($stakeholder['Approved']) {
                                    echo '<span style="color: #4CAF50; font-weight: bold;">✓ Approved</span>';
                                    if ($stakeholder['Approved_date']) {
                                        echo ' on ' . date('d.m.Y H:i', strtotime($stakeholder['Approved_date']));
                                    }
                                } else {
                                    echo '<span style="color: #FF9800; font-weight: bold;">○ Pending Approval</span>';
                                }
                                echo '</div>';
                            }
                            ?>
                        </div>
                    </div>
                    <?php endif; ?>

                    <?php
                    // Get comments
                    $comments_sql = "SELECT c.*,
                                            CASE 
                                                WHEN c.User_type = 'internal' THEN iu.First_name
                                                WHEN c.User_type = 'external' THEN eu.First_name
                                            END as First_name,
                                            CASE 
                                                WHEN c.User_type = 'internal' THEN iu.Last_name
                                                WHEN c.User_type = 'external' THEN eu.Last_name
                                            END as Last_name
                                     FROM comments c
                                     LEFT JOIN internal_user iu ON c.User_ID = iu.Int_User_ID AND c.User_type = 'internal'
                                     LEFT JOIN external_user eu ON c.User_ID = eu.Ext_User_ID AND c.User_type = 'external'
                                     WHERE c.Contract_NR = ?
                                     ORDER BY c.Comment_date DESC";
                    
                    $comments_stmt = $conn->prepare($comments_sql);
                    $comments_stmt->bind_param("i", $selected_contract_id);
                    $comments_stmt->execute();
                    $comments_result = $comments_stmt->get_result();

                    if ($comments_result->num_rows > 0):
                    ?>
                    <!-- Comments Section -->
                    <div class="comments-section">
                        <h3>Comments & Discussion</h3>
                        <?php
                        while($comment = $comments_result->fetch_assoc()) {
                            $comment_class = 'comment-' . $comment['User_type'];
                            $user_type_label = ucfirst($comment['User_type']);
                            
                            echo '<div class="comment ' . $comment_class . '">';
                            echo '<div class="comment-header">';
                            echo htmlspecialchars($comment['First_name'] . ' ' . $comment['Last_name']);
                            echo ' <span style="font-size: 11px; color: #888;">(' . $user_type_label . ' User)</span>';
                            echo '</div>';
                            echo '<div class="comment-date">';
                            echo date('d.m.Y H:i', strtotime($comment['Comment_date']));
                            echo '</div>';
                            echo '<div>' . nl2br(htmlspecialchars($comment['Comment_text'])) . '</div>';
                            echo '</div>';
                        }
                        ?>
                    </div>
                    <?php endif; ?>

                    <!-- Signature Section -->
                    <div class="signature-section">
                        <p><strong>This contract has been reviewed and agreed upon by all parties.</strong></p>
                        
                        <div style="display: grid; grid-template-columns: 1fr 1fr; gap: 40px; margin-top: 60px;">
                            <div>
                                <div class="signature-line">
                                    <div><strong>Company Representative</strong></div>
                                    <div style="font-size: 12px; color: #666;">
                                        <?php echo htmlspecialchars($contract['Company_name']); ?>
                                    </div>
                                    <div style="font-size: 11px; color: #888; margin-top: 5px;">Date: _____________</div>
                                </div>
                            </div>
                            <div>
                                <div class="signature-line">
                                    <div><strong>Internal Representative</strong></div>
                                    <div style="font-size: 12px; color: #666;">
                                        <?php echo htmlspecialchars($contract['Creator_First'] . ' ' . $contract['Creator_Last']); ?>
                                    </div>
                                    <div style="font-size: 11px; color: #888; margin-top: 5px;">Date: _____________</div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <?php
            } else {
                echo '<div class="a4-paper"><p class="no-contract">Contract not found.</p></div>';
            }
        } else {
            echo '<div class="a4-paper"><p class="no-contract">Please select a contract to view.</p></div>';
        }

        $conn->close();
        ?>
    </div>
</body>
</html>